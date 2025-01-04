#Make sure to sudo pip3 install flask
from flask import *

app=Flask("Test")

def hello():
    return "Hello from Google Cloud!"

if __name__ == '__main__':
	app.run(debug=True, host='0.0.0.0', port=80)