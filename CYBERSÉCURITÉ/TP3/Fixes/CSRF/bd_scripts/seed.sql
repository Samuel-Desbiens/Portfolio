-- Drop the user table if it exists
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS faq;
DROP TABLE IF EXISTS csrf;

-- Create user table
CREATE TABLE IF NOT EXISTS users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL,
    password TEXT NOT NULL,
    email TEXT NOT NULL,
    solde NUMERIC DEFAULT 0,
    randid INTEGER DEFAULT (1 + ABS(RANDOM()) % 100000),
    role INTEGER DEFAULT 1 -- 1 for clients and 0 for admins
);

CREATE TABLE IF NOT EXISTS faq (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    `message` TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS csrf (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ip TEXT NOT NULL,
    content TEXT NOT NULL
);

-- Inserting users with the details (including email)
INSERT INTO users (username, password, email, solde, role)
VALUES ('Boromir', '420aa9d954a5226106ef6f6acb9312ed', 'boromir@example.com', 0, 0);

INSERT INTO users (username, password, email, solde, role)
VALUES ('FLAG-183729', '0a27898c80353498414ece5f663a7307', 'flagxyz@example.com', 0, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Pippin', '8ab0eb3cf94d9ba90b2ec2e1e3323115', 'pippin@example.com', 300, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Gandalf', 'b7bf121a175c440bc1dffb9ac591b211', 'gandalf@example.com', 800, 1);

INSERT INTO faq (message)
VALUES ('this is test message for my faq table! please disregard.');

INSERT INTO faq (message)
VALUES ('this is second test message for my faq table! please disregard.');
