# C#Forintern

Requirements (Full working)
-------------------------------
Visual Studio 2022
SQL Server
DevExpress
---------------------
Sql script

use ForTest;
CREATE TABLE PRODUCTSALES (
SALEID INT PRIMARY KEY,
PRODUCTCODE NVARCHAR(20),
PRODUCTNAME NVARCHAR(100),
QUANTITY INT,
UNITPRICE DECIMAL(18,2),
SALEDATE DATE
);
-- Sample Data
INSERT INTO PRODUCTSALES (SALEID, PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE)
VALUES
(1, 'P001', 'Pen', 10, 1.50, '2025-06-20'),
(2, 'P001', 'Pen', 5, 1.50, '2025-06-25'),
(3, 'P002', 'Notebook', 3, 3.20, '2025-06-21'),
(4, 'P003', 'Eraser', 15, 0.80, '2025-06-22');

CREATE PROCEDURE usp_GetProductSales(
    @STARTDATE DATE,
    @ENDDATE DATE,
    @PRODUCTNAME NVARCHAR(100))
AS
BEGIN
    SELECT PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE
    FROM PRODUCTSALES
    WHERE SALEDATE BETWEEN @STARTDATE AND @ENDDATE
      AND (@PRODUCTNAME = '' OR PRODUCTNAME LIKE '%' + @PRODUCTNAME + '%')
END

----------------------------------------------------------------------------------
Setup
1. Import `SQLQuery.sql` in SQL Server 
2. Open solution in Visual Studio
3. Update connection string in `SaleRepository.cs` if needed
Connection String
private readonly string _connectionString = "server=DESKTOP-CLBSF3Q\\MSSQLSERVERYUT;database=ForTest;user id=sa;password=123";
