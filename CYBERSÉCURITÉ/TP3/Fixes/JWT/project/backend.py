import dbManager
from flask import *
import traceback
import jwt
import hashlib
import datetime

app = Flask('Bank management back end')
app.config['SECRET_KEY'] = 'ThatSuperSecretKeyOffCourseWinkWink'
@app.route('/createuser', methods=['POST'])
def create_user():
    try:
        data = request.get_json()
        username = data.get('username')
        password = data.get('password')
        hashedPassword = hashlib.md5(password.encode("utf-8")).hexdigest()
        email = data.get('email')

        if not username or not password:
            return jsonify({'error': 'Username and password are required'}), 400

        user_id = dbManager.create_user(username, hashedPassword, email)

        return jsonify({'user_id': user_id}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500


@app.route('/getspecificuser', methods=['GET'])
def get_specific_user():
    try:
        data = request.get_json()
        username = data.get('search')

        if not username:
            return jsonify({'error': 'A name is required'}), 400

        user_id = dbManager.get_specific_user(username)

        return jsonify({'no_banque': user_id}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500

@app.route('/getusers', methods=['GET'])
def get_all_users():
    try:
        users = dbManager.get_all_users()
        return jsonify({'Users':users})
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500

@app.route('/login', methods=['POST'])
def login():
    try:
        data = request.get_json()
        username = data.get('username')
        password = data.get('password')
        ip = data.get('ip')
        hashedPassword = hashlib.md5(password.encode("utf-8")).hexdigest()
        success = dbManager.get_user_by_username(username, hashedPassword)

        encoded = jwt.encode({'User':success[1] , 'Password':success[2],'Solde': success[4], 'Account number': success[5], 'Role':success[6], 'exp':datetime.datetime.utcnow() + datetime.timedelta(minutes=2)}, app.config['SECRET_KEY'], algorithm="HS256")
        
        #Quand un utilisateur se connecte on créer un jeton et on lui assigne
        dbManager.create_token(username, encoded, ip)

        if success:
            return jsonify({'success': 'Authentication successful!', 'user': encoded})
        else:
            return jsonify({'error': 'Invalid username or password'}), 401
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500

@app.route('/transfer', methods=['POST', 'GET', 'PUT'])
def transfer():
    try:
        data = request.get_json()
        accountNumber = data.get('accountNumber')
        targetAccount = data.get('targetAccount')
        ammount = data.get('ammount')

        success = dbManager.transfer_money(accountNumber, targetAccount, ammount)
        return jsonify({'message_id': success}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500

@app.route('/addfaq',methods=['POST'])
def post_faq():
    try:
        data = request.get_json()
        message = data.get('message')
        success = dbManager.add_faq(message)
        return jsonify({'message_id': success}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500


@app.route('/getfaqmessage', methods=['GET'])
def get_all_faq():
    try:
        messages = dbManager.get_all_faq()
        return jsonify({'Message':messages})
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500

@app.route('/getsolde', methods=['GET'])
def get_solde():
    try:
        data = request.get_json()
        accountNumber = data.get('accountNumber')
        solde = dbManager.get_solde(accountNumber)
        return jsonify({'solde':solde})
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500    

@app.route('/checktoken', methods=['GET'])
def check_token():
    try:
        data = request.get_json()
        token = data.get('token')
        userName = data.get('username')
        ip = data.get('ip')
        validToken = dbManager.check_token(userName,token, ip)
        return jsonify({'validity': validToken}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500  

@app.route('/deletetoken', methods=['GET'])
def delete_token():
    try:
        data = request.get_json()
        token = data.get('token')
        validToken = dbManager.delete_token(token)
        return jsonify({'message_id': "deleted"}), 201
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500  

if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5555)
    