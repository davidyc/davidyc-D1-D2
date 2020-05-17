IF NOT EXISTS (SELECT * FROM SYS.TABLES SysTables WHERE SysTables.Name = 'EmployeeCreditCards')
BEGIN
    CREATE TABLE EmployeeCreditCards(
        CreditCardId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        ExpirationDate DATETIME DEFAULT(NULL),
        CardHolderName VARCHAR(200) NOT NULL,
        EmployeeId INT REFERENCES Employees(EmployeeID)
        );
END