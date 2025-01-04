-- Drop the user table if it exists
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS faq;

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

-- Inserting users with the details (including email)
INSERT INTO users (username, password, email, solde, role)
VALUES ('Boromir', '$2b$12$WWm5kV3LlP7K7ypgkmLaWuMOWENoxJgv76N9fG5V4bewwP9HynENG', 'boromir@example.com', 0, 0);

INSERT INTO users (username, password, email, solde, role)
VALUES ('FLAGXYZ', '$2b$12$jQvvcNvg8qjXHguPlt5cFutOpsNySv19/.2bRnzMiydNIJXOU6Wfa', 'flagxyz@example.com', 0, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Pippin', '$2b$12$vnf8g8H8XA2te8Oveqmp7OCf5mEFAyp1bWo09ssi11li7DQfU51Yy', 'pippin@example.com', 300, 1);

INSERT INTO users (username, password, email, solde, role)
VALUES ('Gandalf', '$2b$12$fkEe1wr1JA3LuQJcVH4QdOIj1AIA73nU9deS2YIt1FqHZOyFQv9tu', 'gandalf@example.com', 800, 1);

INSERT INTO faq (message)
VALUES ('this is test message for my faq table! please disregard.');

INSERT INTO faq (message)
VALUES ('this is second test message for my faq table! please disregard.');
