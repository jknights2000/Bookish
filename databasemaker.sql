DROP TABLE if EXISTS Borrowed;
DROP TABLE if EXISTS Accounts;
DROP TABLE if EXISTS Books;
DROP TABLE if EXISTS BookInfo;
CREATE TABLE Accounts(

ID int NOT NULL PRIMARY KEY,
AccountName varchar(255),
AccountPassword varchar(255),

);

CREATE TABLE BookInfo(
ISBN int NOT NULL PRIMARY KEY,
BookName varchar(255),
Author varchar(255),
BarCode int
);
CREATE TABLE Books(
ID int NOT NULL PRIMARY KEY,
ISBN int NOT NULL FOREIGN KEY REFERENCES BookInfo(ISBN)
);
CREATE TABLE Borrowed(
userID int NOT NULL FOREIGN KEY REFERENCES Accounts(ID),
bookID int NOT NULL FOREIGN KEY REFERENCES Books(ID),
duedate datetime,
CONSTRAINT PK_Borrowed PRIMARY KEY (userID,bookID)
);

