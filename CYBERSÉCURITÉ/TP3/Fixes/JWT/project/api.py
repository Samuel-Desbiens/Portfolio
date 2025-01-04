from flask import *
#from myCode.customException import *
import json
import sys
import requests
import jwt

app = Flask('Bank Management Program')
DATABASE = 'banking.db'

BACK_END_IP = 'dbservice'
BACK_END_PORT = '5555'

PIPPIN_FLAG = 'FLAG-0451'
 
def html(message, token= None):
    #If no token is found
    if get_user_role(token) == -1:
        return f""" 
        
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Banking App</title>
            <style>
                body {{
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                margin: 0;
                background-color: #f4f4f4;
                font-family: Arial, sans-serif;
            }}

            form {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 400px;
                width: 100%;
                position: relative;
                margin-bottom: 20px;
            }}

            .message-box {{
                background-color: #d4edda;
                color: #155724;
                padding: 10px 10px;
                border-radius: 4px;
                margin-bottom: 16px;
                margin: 10px 10px;
            }}

            label {{
                display: block;
                margin-bottom: 8px;
            }}

            input {{
                width: 100%;
                padding: 8px;
                margin-bottom: 16px;
                box-sizing: border-box;
            }}

            button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 10px 15px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 16px;
                margin-bottom: 16px; /* Add margin between the form and the search field */
            }}
            
            .search-container {{
                display: flex;
                flex-direction: column;
                align-items: center;
            }}

            input.search {{
                width: 100%;
                padding: 8px;
                margin-bottom: 8px;
                box-sizing: border-box;
            }}

            button.search-button, button.fetch-button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 8px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 14px;
            }}
            </style>
        </head>

        <body>
            <h1> Welcome to the awesome *Not broken* App! </h1>
            <div class="message-box"> {message} </div>

            <form action="/login" method="POST">
                <label for="loginName">Name:</label>
                <input type="text" id="loginName" name="loginName" required>

                <label for="loginPassword">Password:</label>
                <input type="password" id="loginPassword" name="loginPassword" required>

                <button type="submit">Login</button>
            </form>

            <form action="/getspecificuser" method="GET" class="search-container">
                <label for="search">Search for a user</label>
                <input type="text" class="search" id="search" name="search" placeholder="Search...">
                <button class="search-button" type="submit">Search</button>
            </form>
        </body>
        </html>
           
        """
    #For admin   
    elif get_user_role(token) == 0:
        return f""" 
        
        <!DOCTYPE html>
            <html lang="en">
            <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Banking App</title>
            <style>
                body {{
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                margin: 0;
                background-color: #f4f4f4;
                font-family: Arial, sans-serif;
            }}

            form {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 400px;
                width: 100%;
                position: relative;
                margin-bottom: 20px;
            }}

            .message-box {{
                background-color: #d4edda;
                color: #155724;
                padding: 10px 10px;
                border-radius: 4px;
                margin-bottom: 16px;
                margin: 10px 10px;
            }}

            label {{
                display: block;
                margin-bottom: 8px;
            }}

            input {{
                width: 100%;
                padding: 8px;
                margin-bottom: 16px;
                box-sizing: border-box;
            }}

            button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 10px 15px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 16px;
                margin-bottom: 16px; /* Add margin between the form and the search field */
            }}
            
            .search-container {{
                display: flex;
                flex-direction: column;
                align-items: center;
            }}

            input.search {{
                width: 100%;
                padding: 8px;
                margin-bottom: 8px;
                box-sizing: border-box;
            }}

            button.search-button, button.fetch-button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 8px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 14px;
            }}
            </style>
        </head>

            <body>
            <h1> Welcome to the awesome *Not broken* App! </h1>
            <div class="message-box"> {message} </div>
            
            <form action="/register" method="POST">
                <center >Create an account</center>

                <label for="name">Name:</label>
                <input type="text" id="name" name="name" required>

                <label for="email">Email:</label>
                <input type="email" id="email" name="email" required>

                <label for="password">Password:</label>
                <input type="password" id="password" name="password" required>

                <button type="submit">Register</button>
            </form>

            <form action="/logout" method="POST">
                <h2 >Logout</h2>

                <button type="submit">Logout</button>
            </form>
            </body>

            </html>
           
        """
    #For standard users
    elif get_user_role(token) == 1:
        return f""" 
        
        <!DOCTYPE html>
            <html lang="en">
            <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Banking App</title>
            <style>
                body {{
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                margin: 0;
                background-color: #f4f4f4;
                font-family: Arial, sans-serif;
            }}

            form {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 400px;
                width: 100%;
                position: relative;
                margin-bottom: 20px;
            }}

            .message-box {{
                background-color: #d4edda;
                color: #155724;
                padding: 10px 10px;
                border-radius: 4px;
                margin-bottom: 16px;
                margin: 10px 10px;
            }}

            label {{
                display: block;
                margin-bottom: 8px;
            }}

            input {{
                width: 100%;
                padding: 8px;
                margin-bottom: 16px;
                box-sizing: border-box;
            }}

            button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 10px 15px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 16px;
                margin-bottom: 16px; /* Add margin between the form and the search field */
            }}
            
            .search-container {{
                display: flex;
                flex-direction: column;
                align-items: center;
            }}

            input.search {{
                width: 100%;
                padding: 8px;
                margin-bottom: 8px;
                box-sizing: border-box;
            }}

            button.search-button, button.fetch-button {{
                width: 100%;
                background-color: #007BFF;
                color: #fff;
                padding: 8px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                font-size: 14px;
            }}
            </style>
        </head>

            <body>
            <h1> Welcome to the awesome *Not broken* App! </h1>
            <div class="message-box"> {message} </div>

            <h2>Account details</h2>
            <form action="/" method="get">
                <button type="submit">Show account details</button>
            </form>

            <h2>Transfer money</h2>
            <form action="/transfer" method="POST">
                <label for="targetAccount">Account number of target:</label>
                <input type="number" id="targetAccount" name="targetAccount" required>

                <label for="ammount">Amount:</label>
                <input type="number" id="ammount" name="ammount" required>

                <button type="submit">Transfer</button>
            </form>

            <h2>Add a Message</h2>
            <form action="/addfaq" method="post">
                <label for="message">Message:</label>
                <input type="text" id="message" name="message" required>
                <br>
                <button type="submit" id="submit-faq-message-button">Add Message</button>
            </form>

            <h2>FAQ Messages</h2>
            <form action="/showfaqmessages" method="get">
                <button type="submit">Show FAQ Messages</button>
            </form>

            <form action="/logout" method="POST">
                <h2 >Logout</h2>

                <button type="submit">Logout</button>
            </form>
            </body>

            </html>
           
        """

@app.route('/')
def welcome():
    cookie = get_token()

    if get_user_role(cookie) == 1:
        decoded_token =  jwt.decode(cookie, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")
        accountNumber = decoded_token.get('accountNumber', decoded_token['Account number'])

        data = {'accountNumber' : accountNumber}
        response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getsolde', json=data)
        soldeObj = json.loads(response.content.decode('utf-8'))

        userName = decoded_token.get('user', decoded_token['User'])
        accountNumber = decoded_token.get('accountNumber', decoded_token['Account number'])

        if (userName == 'Pippin'):
            return html("Welcome " + userName + ". You have " + str(soldeObj['solde']) + "$ At account number : " + str(accountNumber) +". Here's your flag : " + PIPPIN_FLAG , cookie)
        
        

        return html("Welcome " + userName + ". You have " + str(soldeObj['solde']) + "$ At account number : " + str(accountNumber), cookie)
    else:
        return html("Welcome!")

def build_response(response, token= None):
	if response.status_code == 200:
		obj = json.loads(response.content.decode('utf-8'))
		resp = make_response(html(obj, token))
		return resp
	else:
		msg = response.content.decode('utf-8')
		resp = make_response(html({'message':msg}, token))
		return resp

#seulement pour les admins
@app.route('/register', methods=['POST'])
def register():
    data = {'username': request.form['name'], 'password':request.form['password'], 'email':request.form['email']}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/createuser', json=data)
    return build_response(response, get_token())

@app.route('/login', methods=['POST'])
def login():
    ip = request.remote_addr
    data = {'username': request.form['loginName'], 'password':request.form['loginPassword'], 'ip': ip}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/login', json=data)
    
    obj = json.loads(response.content.decode('utf-8'))
    print(obj, flush=True)

    if 'success' in obj:
        resp = build_response(response, obj['user'])
        resp.set_cookie('token', obj['user'])
    elif 'error' in obj:
        resp = build_response(response)

    return resp

@app.route('/logout', methods=['POST'])
def logout():
    resp = make_response(html("Logged out"))

    token = get_token()
    data = {'token' : token}
    resp.delete_cookie('token')
    response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/deletetoken', json=data)

    return resp

@app.route('/getspecificuser', methods=['GET', 'POST'])
def getspecificuser():
    data = {'search': request.args.get('search')}
    response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getspecificuser', json=data)
    return build_response(response, get_token())

@app.route('/getusers', methods=['GET', 'POST'])
def getalluser():
    response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getusers')
    return build_response(response, get_token())

@app.route('/transfer', methods=['GET', 'POST'])
def transfer():
    cookie = request.cookies.get('token')
    decoded_token =  jwt.decode(cookie, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")

    accountNumber = decoded_token.get('accountNumber', decoded_token['Account number'])

    data = {'accountNumber' : accountNumber, 'targetAccount': request.form['targetAccount'], 'ammount':request.form['ammount']}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/transfer', json=data)
    return build_response(response, get_token())

@app.route('/addfaq', methods=['POST'])
def post_faq():
    #get FAQ form data and post to DB
    cookie = get_token()
    data = {'message': request.form['message']}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/addfaq', json=data)
    return build_response(response, get_token())

@app.route('/showfaqmessages', methods=['GET'])
def get_all_faq():
    response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getfaqmessage')
    return build_response(response, get_token())

def get_user_role(token = None):

    if token is None:
        #Devrait retourner -1 pour non identifier
        return -1

    #Check if token exists
    try:
        decoded_token =  jwt.decode(token, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")
        userName = decoded_token.get('user', decoded_token['User'])

        ip = request.remote_addr

        #Simulate other ip
        #ip = '111.0.0.2'

        data = {'token' : token, 'username' : userName, 'ip': ip}

        #On vérifie que le jeton est encore dans la BD, qu'il est encore valide, et que le ip 
        #de l'utilisateur est le même que celui sur le jeton
        response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/checktoken', json=data)
        obj = json.loads(response.content.decode('utf-8'))

        #Si le jeton est invalide, l'utilisateur est ramené a la page pour les utilisateurs non-connecté
        if obj['validity'] != 'Success':
            return -1
            
    
        decoded_token =  jwt.decode(token, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")
        user_role = decoded_token.get('role', decoded_token['Role'])
        return user_role
    except jwt.ExpiredSignatureError:
        #If jwt is expired Log out and delete token from DB
        resp = make_response(html("Logged out"))
        token = get_token()
        data = {'token' : token}
        resp.delete_cookie('token')
        response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/deletetoken', json=data)
        return resp

def get_token():
    cookie = request.cookies.get('token')
    return cookie


if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5556)
