Use Northwind

/*
Задание 1.1. Простая фильтрация данных.
1.  Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) 
включительно и которые доставлены с ShipVia >= 2
Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia. 
*/

SELECT OrderID, ShippedDate, ShipVia FROM Orders 
WHERE ShippedDate >='1998-05-06' AND 
ShipVia >= 2

/*
2.  Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ (использовать системную функцию CASЕ). 
Запрос должен возвращать только колонки OrderID и ShippedDate.
*/

SELECT OrderID,
CASE
    WHEN ShippedDate IS NULL THEN 'Not Shipped'     
END AS ShippedDate
FROM Orders 
WHERE
ShippedDate IS NULL

/*
3.  Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту 
датукоторые еще не доставлены. 
В запросе должны возвращаться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать 
в Shipped Date). 
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, для остальных 
значений возвращать дату в формате по умолчанию.
*/

SELECT OrderID AS "Order Number",
CASE
    WHEN ShippedDate IS NULL THEN 'Not Shipped'    
    WHEN ShippedDate IS NOT NULL THEN convert(varchar, ShippedDate) 
    END AS "Shipped Date"
FROM Orders 
WHERE
ShippedDate IS NULL
OR 
ShippedDate >= '1998-05-06'


/*
Задание 1.2. Использование операторов IN, DISTINCT, ORDER BY, NOT
1.	Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. Запрос сделать с только помощью 
оператора IN. Возвращать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания.
*/
SELECT ContactName, Country FROM Customers 
WHERE Country IN('USA', 'CANADA')
ORDER BY ContactName, Country 

/*
2.	Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора
 IN. Возвращать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты 
 запроса по имени заказчиков.
*/
SELECT ContactName, Country FROM Customers 
WHERE Country NOT IN('USA', 'CANADA')
ORDER BY ContactName 

/*
3.	Выбрать из таблицы Customers все страны, в которых проживают заказчики. Страна должна быть упомянута только 
один раз и список отсортирован по убыванию. Не использовать предложение GROUP BY. Возвращать только одну колонку 
в результатах запроса. 
*/
SELECT DISTINCT Country FROM Customers 
ORDER BY Country DESC 

/*
Задание 1.3. Использование оператора BETWEEN, DISTINCT
1.	Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), где встречаются продукты 
с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. Использовать оператор BETWEEN.
 Запрос должен возвращать только колонку OrderID.
*/
SELECT DISTINCT OrderID FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

/*
2.	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. 
Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен возвращать только 
колонки CustomerID и Country и отсортирован по Country.
*/
SELECT CustomerID, Country FROM Customers 
WHERE Country BETWEEN 'b' AND 'h'
OR Country BETWEEN 'B' AND 'H'
ORDER BY Country

/*
3.	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, 
не используя оператор BETWEEN. 
*/
SELECT CustomerID, Country FROM Customers 
WHERE Country LIKE 'b%'   
OR Country LIKE 'B%'
OR Country LIKE 'g%'
OR Country LIKE 'G%'
ORDER BY Country

/*
Задание 1.4. Использование оператора LIKE
1.	В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты, 
которые удовлетворяют этому условию. 
*/
SELECT ProductName FROM Products
WHERE ProductName LIKE 'cho_olade'

