DROP TABLE if EXISTS Borrowed
DROP TABLE if EXISTS Accounts
DROP TABLE if EXISTS Books
DROP TABLE if EXISTS BookInfo

CREATE TABLE Accounts(
ID int NOT NULL PRIMARY KEY,
AccountName varchar(255),
AccountPassword varchar(255),
)

CREATE TABLE BookInfo(
ISBN int NOT NULL PRIMARY KEY,
BookName varchar(255),
Author varchar(255),
BarCode int
)
CREATE TABLE Books(
ID int NOT NULL PRIMARY KEY,
ISBN int NOT NULL FOREIGN KEY REFERENCES BookInfo(ISBN)
)
CREATE TABLE Borrowed(
UserID int NOT NULL FOREIGN KEY REFERENCES Accounts(ID),
BookID int NOT NULL FOREIGN KEY REFERENCES Books(ID),
Duedate datetime,
CONSTRAINT PK_Borrowed PRIMARY KEY (userID,bookID)
);

BULK INSERT Accounts
FROM 'C:\Users\Jim.Davey\Work\Bookish\Bookish\AccountsData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT BookInfo
FROM 'C:\Users\Jim.Davey\Work\Bookish\Bookish\BookInfoData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT Books
FROM 'C:\Users\Jim.Davey\Work\Bookish\Bookish\BooksData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT Borrowed
FROM 'C:\Users\Jim.Davey\Work\Bookish\Bookish\BorrowedData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)
select * from Accounts;
select * from BookInfo;
select * from Books;
select * from Borrowed;