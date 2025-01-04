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
    #Ajout des gabarits afin de fixe le type d'Attaque XSS, nous avons cru bons d'en mettre 1 pour chaque partie de notre site web.

    #If no token is found
    if get_user_role(token) == -1:
        return render_template('home.html', message=message)

    # For admin   
    elif get_user_role(token) == 0:
        return render_template('admin.html', message=message)

    # For standard users
    elif get_user_role(token) == 1:
        return render_template('user.html', message=message)

@app.route('/')
def welcome():
    cookie = get_token()

    if get_user_role(cookie) == 1:
        decoded_token =  jwt.decode(cookie, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")
        accountNumber = decoded_token.get('accountNumber', decoded_token['Account number'])
        data = {'accountNumber' : str(accountNumber)}
        response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getsolde', json=data)
        soldeObj = json.loads(response.content.decode('utf-8'))
        userName = decoded_token.get('user', decoded_token['User'])
        accountNumber = decoded_token.get('accountNumber', decoded_token['Account number'])
        return html("Welcome " + userName + ". You have " + str(soldeObj['solde']) + "$ At account number : " + str(accountNumber), cookie)
    else:
        return html("Welcome!")

def build_response(response, token=None):
    print("API Response Status Code:", response.status_code, flush=True)
    print("API Response Content:", response.content.decode('utf-8'), flush=True)

    if response.status_code == 200:
        obj = json.loads(response.content.decode('utf-8'))
        print("Response Object:", obj, flush=True)
        resp = make_response(html(obj, token))
        print("Constructed HTML Response:", resp, flush=True)
        return resp
    else:
        msg = response.content.decode('utf-8')
        print("Error Message:", msg, flush=True)
        resp = make_response(html({'message': msg}, token))
        print("Constructed HTML Response:", resp, flush=True)
        return resp

#seulement pour les admins
@app.route('/register', methods=['POST'])
def register():
    data = {'username': request.form['name'], 'password':request.form['password'], 'email':request.form['email']}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/createuser', json=data)
    return build_response(response, get_token())

@app.route('/login', methods=['POST'])
def login():
    data = {'username': request.form['loginName'], 'password': request.form['loginPassword']}
    response = requests.post('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/login', json=data)

    obj = json.loads(response.content.decode('utf-8'))
    print("Json Content:", obj, flush=True)
    if 'success' in obj:
        resp = build_response(response, obj['user'])
        #rajout de HTTPOnly sur le cookie afin que le javascript ne soit pas en mesure de toucher a celui-ci.
        resp.set_cookie('token', obj['user'], httponly=True)
        return resp
    elif 'error' in obj:
        resp = build_response(response)
        return resp
    else:
        return make_response(html({'message': 'Unexpected error'}, get_token()))

@app.route('/logout', methods=['POST'])
def logout():
    resp = make_response(html("Logged out"))

    resp.delete_cookie('token')

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
    try:
        decoded_token =  jwt.decode(token, "ThatSuperSecretKeyOffCourseWinkWink", algorithms="HS256")
        user_role = decoded_token.get('role', decoded_token['Role'])
        print("role: " ,user_role, flush=True)
        return user_role
    except jwt.ExpiredSignatureError:
        return "Token has expired"
        return "Invalid token"

def get_token():
    cookie = request.cookies.get('token')
    return cookie


if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5556)
