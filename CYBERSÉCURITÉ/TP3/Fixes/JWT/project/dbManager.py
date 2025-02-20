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
        #check if user exist in DB
        cursor.execute("INSERT INTO users (username, password, email) VALUES (?, ? , ?)", 
                       (username, password, email))
        conn.commit()
        return cursor.lastrowid
    except Error as e:
        print(e)
        return None
    finally:
        conn.close()

def get_specific_user(username):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        #cursor.execute("SELECT randid FROM users WHERE username=" + username + ")")
        cursor.execute("SELECT randid FROM users WHERE username=?", (username,)) #  <-- fixed code
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

def create_token(username, token, ip):
    conn = create_connection()
    try:
        cursor = conn.cursor()

        cursor.execute("INSERT INTO tokens (username, content, ip) VALUES (?, ?, ?)", (username, token, ip))
        conn.commit()
        return "Success"
    except Error as e:
        print(e)
        return None
    finally:
         conn.close()

def check_token(username, token, ip):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("SELECT id FROM tokens WHERE username =? AND content =? AND ip =?", (username, token, ip))

        if not cursor.fetchone():
            return "User and token do not match"

        return "Success"
    except Error as e:
        print(e)
        return None
    finally:
         conn.close()    

def delete_token(token):
    conn = create_connection()
    try:
        cursor = conn.cursor()
        cursor.execute("DELETE FROM tokens WHERE content = ?", (token,))
        conn.commit()

        return "Success"
    except Error as e:
        print(e)
        return None
    finally:
         conn.close()      