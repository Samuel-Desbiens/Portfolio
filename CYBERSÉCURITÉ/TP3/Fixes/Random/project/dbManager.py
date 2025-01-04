import sqlite3
from sqlite3 import Error

def create_connection():
    db_file = "./project/banking.db"
    conn = None
    try:
        conn = sqlite3.connect(db_file)
    except Error as e:
        print(e)
    return conn


def create_user(username, password, email):
    conn = create_connection()
    try:
        cursor = conn.cursor()

        cursor.execute("SELECT 1 FROM users WHERE username = ? LIMIT 1", (username,))
        if cursor.fetchone():
            return "Source account already exist"

        cursor.execute("INSERT INTO users (username, password, email) VALUES (?, ? , ?)", 
                       (username, password, email))
        conn.commit()
        return cursor.lastrowid
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()
        
#MOD    
def change_password(username,newPassword):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("UPDATE users SET password = ? WHERE username = ?",(newPassword,username))
        conn.commit()
        print("fuckup in change_password returns")
        return "Success"
    except Error as e:
        print(e)
        conn.rollback()
        return None
    finally:
        conn.close()

#WOULD BE USEFULL IF TWILLIO WORKED
"""
def get_email_from_account_number(accountNumber):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT email FROM users WHERE accountNumber = ?",(accountNumber))
        result = cursor.fetchone()
        if result:
            return result[3]
        else:
            return None
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()
"""
     
def get_code(username):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT id FROM users WHERE username = ?",(username,))
        userID = cursor.fetchone()[0]
        cursor.execute("SELECT activeCode FROM code WHERE userID = ?",(userID,))
        code = cursor.fetchone()[0]
        if code:
            cursor.execute("DELETE FROM code WHERE userID = ?",(userID,))
            conn.commit()
            return code
        else:
            return None
    except Error as e:
        print(e)
        conn.rollback()
        return None
    finally:
        conn.close()
        
def set_code(code,username):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT id FROM users WHERE username = ?",(username,))
        userID = cursor.fetchone()[0]
        cursor.execute("SELECT * FROM code WHERE userId = ?",(userID,))
        cleanCheck = cursor.fetchall()
        if len(cleanCheck) != 0:
            for row in cleanCheck:
                cursor.execute("DELETE FROM code WHERE id = ?",(row[0],))
        cursor.execute("INSERT INTO code (activeCode, userId) VALUES (?, ?)",(code,userID))
        conn.commit()
        return cursor.lastrowid
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()          
#MOD

def get_specific_user(username):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT randid FROM users WHERE username='" + username + "';")
        #cursor.execute("SELECT randid FROM users WHERE username=?", (username,)) #  <-- fixed code
        result = cursor.fetchone()
        if result:
            return result[0]
        else:
            return None

    except Error as e:
        print(e)
        return None
    finally:
        conn.close()


def get_all_users():
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT username, randid FROM users")
        rows = cursor.fetchall()
        return rows
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()

def get_user_by_username(username, password):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM users WHERE username=?", (username,)) #<-- fixed code, a rendre moins safe pour l'injection SQL
        user = cursor.fetchone()
        if user and user[2] == password:
            user_info = (user[0], user[1], user[2], user[3], user[4], user[5], user[6])
            return user_info
        else:
            return None
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()
    
def transfer_money(accountNumber, targetAccount, ammount):
    conn = create_connection()
    try:
        cursor = conn.cursor()

        cursor.execute("SELECT 1 FROM users WHERE randid = ? LIMIT 1", (accountNumber,))
        if not cursor.fetchone():
            return "Source account does not exist"

        cursor.execute("SELECT 1 FROM users WHERE randid = ? LIMIT 1", (targetAccount,))
        if not cursor.fetchone():
            return "Target account does not exist"

        conn.execute("BEGIN")

        cursor.execute("UPDATE users SET solde = solde - ? WHERE randid = ? AND solde >= ?", (ammount, accountNumber, ammount))

        if cursor.rowcount > 0:
            cursor.execute("UPDATE users SET solde = solde + ? WHERE randid = ?", (ammount, targetAccount))

            conn.commit()
            return "Success"
        else:
            conn.rollback()
            return "Insufficient funds"
    except Error as e:
        print(e)
        conn.rollback()
        return None
    finally:
        conn.close()

def add_faq(message):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO faq (message) VALUES (?)", (message,))
        conn.commit()
        return cursor.lastrowid
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()

def get_all_faq():
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT message FROM faq")
        rows = cursor.fetchall()
        return rows
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()

def get_solde(accountNumber):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT solde FROM users WHERE randid =?", (accountNumber,))
        row = cursor.fetchone()
        return row
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()