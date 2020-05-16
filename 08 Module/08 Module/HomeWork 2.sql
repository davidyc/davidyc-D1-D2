/*
Задание 2.1. Использование агрегатных функций (SUM, COUNT)
1.	Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
*/
SELECT SUM((UnitPrice - (UnitPrice * Discount) )* Quantity ) as Totals FROM [Order Details]

/*
2.	По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate нет 
значения даты доставки). Использовать при этом запросе только оператор COUNT. Не использовать предложения WHERE и 
GROUP.
*/
SELECT COUNT(*) - COUNT(ShippedDate) AS Shipped FROM Orders

/*
3.	По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/
SELECT DISTINCT COUNT(CustomerID) As CustIDUnique FROM Orders

/*
Задание 2.2. Соединение таблиц, использование агрегатных функций и предложений GROUP BY и HAVING 
1.	По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса надо возвращать две 
колонки c названиями Year и Total. Написать проверочный запрос, который вычисляет количество всех заказов.
*/
SELECT YEAR(OrderDate) As Year, COUNT(Orders.RequiredDate) AS YearTotal  FROM Orders GROUP BY  YEAR(OrderDate)

SELECT COUNT(*) AS TotalOrders FROM Orders 

/*
2.	По таблице Orders найти количество заказов, cделанных каждым продавцом. Заказ для указанного продавца – это 
любая запись в таблице Orders, где в колонке EmployeeID задано значение для данного продавца. В результатах запроса
надо возвращать колонку с именем продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и колонку
c количеством заказов возвращать с названием 'Amount'. Результаты запроса должны быть упорядочены по убыванию 
количества заказов. 
*/
SELECT (SELECT CONCAT(Employees.LastName,' ', Employees.FirstName) 
        FROM Employees  
        WHERE Employees.EmployeeID = Orders.EmployeeID) 
     AS 'Seller'
    ,COUNT(OrderId) AS 'Amount'
    FROM Orders  
GROUP BY Orders.EmployeeID
ORDER BY 'Amount' DESC;

/*
3.	По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя.
Необходимо определить это только для заказов, сделанных в 1998 году. 
*/
SELECT EmployeeID, COUNT(CustomerID) as CountOrders  FROM Orders
WHERE OrderDate > '1998'
GROUP BY EmployeeID

/*
4.	Найти покупателей и продавцов, котоdрые живут в одном городе. Если в городе живут только один или 
несколько продавцов, или только один или несколько покупателей, то информация о таких покупателя и 
продавцах не должна попадать в результирующий набор. Не использовать конструкцию JOIN. 
*/
SELECT Customers.CustomerID, EmployeesT.EmployeeID, Customers.City FROM Customers
CROSS APPLY (SELECT EmployeesT.EmployeeId 
                FROM Employees AS EmployeesT 
                WHERE EmployeesT.[City] = Customers.[City]) EmployeesT;

/*
5.	Найти всех покупателей, которые живут в одном городе. 
*/
SELECT Cust.City as City, Cust.CustomerID as Customer, Cust2.CustomerID as Neighor  FROM Customers as Cust
LEFT JOIN Customers as Cust2 ON
Cust.CustomerID <> Cust2.CustomerID and Cust.City = Cust2.City
ORDER BY Cust.City

/*
6.	По таблице Employees найти для каждого продавца его руководителя.
*/
SELECT Empl.EmployeeID as Employees, Empl2.EmployeeID as BOSS  FROM Employees as Empl
LEFT JOIN Employees as Empl2 ON
Empl2.EmployeeID = Empl.ReportsTo

/*Задание 2.3. Использование JOIN
1.	Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
*/
SELECT EmployeeID, Region FROM Employees


/*
2.	Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders. Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. Упорядочить результаты запроса по возрастанию количества заказов.
*/
