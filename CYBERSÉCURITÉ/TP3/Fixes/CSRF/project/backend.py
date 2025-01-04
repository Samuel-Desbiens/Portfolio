import dbManager
from flask import *
import traceback
import jwt
import hashlib
import sendgrid
import os
from sendgrid.helpers.mail import Mail, Email, To, Content
import secrets

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
        ip = data.get('ip')
        success = dbManager.get_user_by_username(username, hashedPassword)


        #qu'est-ce qui est storer dans le token: nom, no_compte, role à ajouter: browser id, expiration time
        encoded = jwt.encode({'User':success[1] , 'Password':success[2],'Solde': success[4], 'Account number': success[5], 'Role':success[6]}, app.config['SECRET_KEY'], algorithm="HS256")
        
        if username == 'Gandalf':
            encoded = jwt.encode({'User':success[1] , 'Password':success[2],'Solde': success[4], 'Account number': success[5], 'Role':success[6], 'Flag':"FLAG-TEST"}, app.config['SECRET_KEY'], algorithm="HS256")
        
        #Créer le csrf dans la db
        createdCsrf = dbManager.create_csrf(ip, secrets.token_urlsafe(64))

        if success:
            return jsonify({'success': 'Authentication successful!', 'user': encoded})
        else:
            return jsonify({'error': 'Invalid username or password'}), 401
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500
        
#MOD
"""
 @app.route('/passwordreset',methods=['POST']')
 def password_reset():
    try:
        data = request.get_json()
        sg = sendgrid.SendGridAPIClient(api_key=os.environ.get('KEYHERE'))
        to_email = To(data.get('email'))
        from_email =Email('bank@email.ca')
        subject = "Password Reset Request"
        content = Content("text/plain","your security code is : CODE")
        mail = Mail(from_email, to_email,subject,content)
        
        response = sg.client.mail.send.post(request_body=mail_json)
        
        
        return jsonify({'message_id': response.headers}),response.status_code
        
        except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500
        
 @app.route('/passwordswitch',methods=['POST']')
 def password_switch();
    try:
        data = request.get_json()
        email = data.get('email')
        newPassword = data.get('newPassword')
        
        success = dbManager.password_switch(email,newPassword)
        
        return jsonify({'message_id': success}), 200
        
        except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}),500
"""  

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

@app.route("/checkcsrf", methods=['GET'])
def check_CSRF():
    try:
        data = request.get_json()
        csrfToken = data.get('csrfToken')
        ip = data.get('ip')

        dbToken = dbManager.get_csrf(ip)

        #Compare les deux token
        #Ne fonctionne pas car le puppet Boromir a le meme ip dans les deux fenêtre
        if(dbToken[0] == csrfToken):
            return jsonify({'message':"success"})

        return jsonify({'message':"error"})
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500   

@app.route("/getcsrf", methods=['GET'])
def get_csrf():
    try:
        data = request.get_json()
        ip = data.get('ip')

        #Va chercher le token basée sur le ip recu
        token = dbManager.get_csrf(ip)

        return jsonify({'message':token})
    except Exception as e:
        print(traceback.format_exc())
        return jsonify({'error': str(e)}), 500 

if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5555)