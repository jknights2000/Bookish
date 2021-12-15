DROP TABLE if EXISTS Borrowed
DROP TABLE if EXISTS Books
DROP TABLE if EXISTS BookInfo

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
userID nvarchar(450) NOT NULL FOREIGN KEY REFERENCES AspNetUsers(ID),
bookID int NOT NULL FOREIGN KEY REFERENCES Books(ID),
duedate datetime,
CONSTRAINT PK_Borrowed PRIMARY KEY (userID,bookID)
);

BULK INSERT BookInfo
FROM 'C:\Users\Francis.Jordan\OneDrive\Desktop\Learning\Bootcamp\Bookish\BookInfoData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT Books
FROM 'C:\Users\Francis.Jordan\OneDrive\Desktop\Learning\Bootcamp\Bookish\BooksData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT Borrowed
FROM 'C:\Users\Francis.Jordan\OneDrive\Desktop\Learning\Bootcamp\Bookish\BorrowedData.csv'
WITH
(
	FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)
select * from AspNetUsers;
select * from BookInfo;
select * from Books;
select * from Borrowed;

UPDATE AspNetUsers set EmailConfirmed = 1 
select ID,ISBN from (
select * from books
left join  Borrowed
on books.id = Borrowed.bookID
where Borrowed.bookID is null
) as ListOfAvailableBooks
where ListOfAvailableBooks.ISBN = 3030;

select isbn from books where id=2