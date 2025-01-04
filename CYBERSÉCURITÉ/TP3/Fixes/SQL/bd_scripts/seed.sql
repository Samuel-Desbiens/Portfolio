-- Drop the user table if it exists
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS faq;
DROP TABLE IF EXISTS attacker;

-- Create user table
CREATE TABLE IF NOT EXISTS users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL,
    password TEXT NOT NULL,
    email TEXT NOT NULL,
    solde NUMERIC DEFAULT 0,
    randid INTEGER DEFAULT (1 + ABS(RANDOM()) % 100000),
    `message` TEXT NULL,
    role INTEGER DEFAULT 1 -- 1 for clients and 0 for admins
);

CREATE TABLE IF NOT EXISTS faq (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    `message` TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS attacker(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    token TEXT NOT NULL
);


-- Inserting users with the details (including email)
INSERT INTO users (username, password, email, solde, role)
VALUES ('Boromir', 'securepassword1234abcdefghijklmnopqrstuvwxyz', 'boromir@example.com', 0, 0);

INSERT INTO users (username, password, email, solde, role)
VALUES ('FLAG-183729', 'randompassword5678abcdefghijklmnopqrstuvw', 'flagxyz@example.com', 0, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Pippin', 'temptest', 'pippin@example.com', 300, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Gandalf', 'strongpassword7890ABCDEFGHJKLMNOPQRSTUV', 'gandalf@example.com', 800, 1);

INSERT INTO faq (message)
VALUES ('this is test message for my faq table! please disregard.');

INSERT INTO faq (message)
VALUES ('this is second test message for my faq table! please disregard.');
