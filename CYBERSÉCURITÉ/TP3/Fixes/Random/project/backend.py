import dbManager
from flask import *
import traceback
import jwt
import hashlib
#Imports For Twillio if Worked
"""
import sendgrid
import os
from sendgrid.helpers.mail import Mail, Email, To, Content
"""
from config import TOKEN_KEY
import sys
import secrets

app = Flask('Bank management back end')
app.config['SECRET_KEY'] = TOKEN_KEY
app.SecretGenerator = secrets.SystemRandom()

@app.route('/createuser', methods=['POST'])
def create_user():
    try:
        data = request.get_json()
        username = data.get('username')
        password = data.get('password')
        hashedPassword = hashlib.md5(password.encode("utf-8")).hexdigest()
        email = data.get('email')

        #To see saruman's credentials
        print(username, flush=True)
        print(email, flush=True)
        print(password, flush=True)


        if not username or not password:
            return jsonify({'error': 'Username and password are required'}), 400

        user_id = dbManager.create_user(username, hashedPassword, email)

        print(user_id, flush=True)

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
        hashedPassword = hashlib.md5(password.encode("utf-8")).hexdigest()
        success = dbManager.get_user_by_username(username, hashedPassword)
        #qu'est-ce qui est storer dans le token: nom, no_compte, role à ajouter: browser id, expiration time

        
        encoded = jwt.encode({'User':success[1] , 'Password':success[2],'Solde': success[4], 'Account number': success[5], 'Role':success[6]}, app.config['SECRET_KEY'], algorithm="HS256")
        
        if username == 'Gandalf':
            encoded = jwt.encode({'User':success[1] , 'Password':success[2],'Solde': success[4], 'Account number': success[5], 'Role':success[6], 'Flag':"FLAG-TEST"}, app.config['SECRET_KEY'], algorithm="HS256")
        
        if success:
            return jsonify({'success': 'Authentication successful!', 'user': encoded})
        else:
            return jsonify({'error': 'Invalid username or password'}), 401
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500
        
#MOD
@app.route('/passwordreset',methods=['POST'])
def password_reset():
    try:
        random_code = app.SecretGenerator.randint(0,sys.maxsize)
        data = request.get_json()
        username = data.get('username')
        success = dbManager.set_code(random_code,username)
        #SI TWILLIO MARCHAIT
        """
        if success:
            data = request.get_json()
            sg = sendgrid.SendGridAPIClient(api_key=os.environ.get('KEYHERE'))
            to_email = To(data.get('email'))
            from_email =Email('bank@email.ca')
            subject = "Password Reset Request"
            content = Content("text/plain","your security code is : " + str(app.BaseRandomCode+app.NumberOfCode))
            mail = Mail(from_email, to_email,subject,content)
        
            response = sg.client.mail.send.post(request_body=mail_json)
        
            return jsonify({'message_id': response.headers}),response.status_code
        """
        if success:
            return jsonify({'message_id':str(random_code)})
        else:
            return None
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500
        
@app.route('/changepassword',methods=['POST'])
def change_password():
    try:
        data = request.get_json()
        username = data.get('username')
        code = data.get('code')
        newPassword = data.get('newPassword')
        newhashedPassword = hashlib.md5(newPassword.encode("utf-8")).hexdigest()
        

        verificationCode = dbManager.get_code(username)
        
        if verificationCode == code:
            success = dbManager.change_password(username,newhashedPassword)
            return jsonify({'message_id': success}), 200
        else:
            return jsonify({'message_id': 'wrong code'}),400
        
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}),500

#WOULD BE USEFULL IF TWILLIO WORKED
"""  
@app.route('/getemailfromaccountnumber',methods=['GET'])
def get_email_from_account_number():
    try:
        data = request.get_json()
        accountNumber = data.get('accountNumber')
        
        success = dbManager.get_email_from_account_number(accountNumber)
        
        return jsonify({'message_id':success}),200
        
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}),500
"""       
#MOD

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

if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5555)
    
