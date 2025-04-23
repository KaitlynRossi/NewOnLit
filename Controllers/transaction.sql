-- SQLite
CREATE TABLE Transactions (
    transID INTEGER PRIMARY KEY AUTOINCREMENT,
    userID INTEGER,
    bookID INTEGER,
    qty INTEGER,
    saleAmount DECIMAL,
    FOREIGN KEY (bookID) REFERENCES Books(Id)
);