--select all records from customer table
select * from customers;

--select column in table
select CustomerName, City from customers;

--sql select distinct statement
--without distinct
select City from Customers;
--with distinct 
select distinct City from Customers;
--SQL statement lists the number of different (distinct) customer cities
select count(distinct City) from customers;

--SQL WHERE Clause
select * from customers where City = 'Berlin';
select * from customers where CustomerID = 11;
select * from customers where CustomerID <= 10;
select * from customers where CustomerID > 10;
select * from customers where CustomerID <> 11;
select * from customers where CustomerID between 5 and 10;
select * from customers where Country like 'UK%';
select * from customers where Country in ('UK', 'Germany');

--SQL AND, OR and NOT Operators
select * from customers where CustomerID <= 10 and City = 'México D.F.';
select * from customers where City = 'Berlin' or City = 'México D.F.';
select * from customers where not Country = 'Germany';
--Combinig and,or,not
select * from customers where Country= 'Germany' and (City = 'Berlin' or City = 'Münster');
select * from customers where CustomerName like 'A%' and (City = 'Berlin');
select * from customers where not Country = 'Germany' and not Country = 'USA';

--SQL ORDER BY Keyword
select * from customers order by CustomerID desc;
select * from customers order by CustomerID asc;
select * from customers order by Country, CustomerName;
select * from customers order by Country ASC, CustomerName DESC;

--SQL INSERT INTO Statement
insert into customers values (100, 'himanshu ladva', 'haladva', 'shapar veraval', 'rajkot', 360024, 'india'); 
insert into customers (CustomerID, CustomerName) values (101, 'RutvikPesivadiya');

--SQL NULL Values
select CustomerName,CustomerID,City, Country from customers where City is null;
select ContactName, Address from customers where Address is null;

--The SQL UPDATE Statement
update customers set CustomerName = 'Universal King', City = 'Newyork' where CustomerID = 1;

--SQL DELETE Statement
delete from customers where CustomerName = 'himanshu ladva';
--DELETE FROM table_name; delete all records

--SQL TOP, LIMIT, FETCH FIRST or ROWNUM Clause
select top 5 * from customers;
select top 10 percent * from customers;
--first three germany person
select top 3 * from customers where Country = 'Germany';

--SQL MIN() and MAX() Functions
select * from products;
select min(Price) as SmallestPrice from products;
select max(Price) as LargestPrice from products;

--SQL COUNT(), AVG() and SUM() Functions
select count(ProductID) from products;
select avg(Price) from products;
select sum(Price) from products;

--SQL LIKE Operator
-- selects all customers with a CustomerName starting with "a":
select * from customers where CustomerName like 'a%';
--selects all customers with a CustomerName ending with "a":
select * from customers where CustomerName like '%a';
--selects all customers with a CustomerName that have "or" in any position:
select * from customers where CustomerName like '%or%';
-- selects all customers with a CustomerName that have "r" in the second position:
select * from customers where CustomerName like '_a%';
--selects all customers with a CustomerName that starts with "a" and are at least 3 characters in length:
select * from customers where CustomerName like 'a__%';
--selects all customers with a ContactName that starts with "a" and ends with "o":
select * from customers where CustomerName like 'A%d';
--cts all customers with a CustomerName that does NOT start with "a":
select * from customers where CustomerName not like 'a%';

--SQL Wildcards
select * from customers where City like '_ondon';
select * from customers where City like 'L_n_on';
--Using the [charlist] Wildcard
--selects all customers with a City starting with "b", "s", or "p":
select * from customers where City like '[bsp]%';
--selects all customers with a City starting with "a", "b", or "c":
select * from customers where City like '[a-c]%';
--select all customers with a City NOT starting with "b", "s", or "p":
select * from customers where City like '[!bsp]%';

--SQL IN Operator
--selects all customers that are located in "Germany", "France" or "UK":
select * from customers where Country in ('Germany', 'France', 'UK');
--with help of or operator
select * from customers where Country = 'Germany' or Country = 'France' or Country = 'UK';
--selects all customers that are NOT located in "Germany", "France" or "UK":
select * from customers where Country not in ('Germany', 'France', 'UK');
--selects all customers that are from the same countries as the suppliers:
select * from customers where Country in (select Country from suppliers);

--SQL BETWEEN Operator
select * from customers where CustomerID between 11 and 20;
select * from customers where CustomerID not between 11 and 20;
--BETWEEN with IN Example
select * from customers where CustomerID between 21 and 30 and Country not in ('Germany', 'France', 'UK'); 
--BETWEEN Text Values Examplee
select * from customers where CustomerName between 'Bon app\' and 'himanshu' order by CustomerName;
select * from products where ProductName between 'Chais' and 'Ikura' order by ProductName;
--BETWEEN Dates Example
select * from Orders where OrderDate between '1996-07-01' and '1996-07-31';

--SQL Aliases
select CustomerName as CName from customers;
select CustomerName as Customer, ContactName as [Contact Name] from customers;
select CustomerName, Address + ', ' +  PostalCode + ' ' + City + ', ' + Country as address from customers;
select o.OrderID, o.OrderDate, c.CustomerName from customers as c, orders as o where c.CustomerName = 'Himanshu Ladva' and c.CustomerID = o.CustomerID;

--SQL Joins
select * from orders;
select orders.OrderID, customers.CustomerName, orders.OrderDate from orders inner join customers on orders.CustomerID = customers.CustomerID;
select orders.OrderDate, customers.City, orders.ShipperID from orders inner join customers on orders.CustomerID = customers.CustomerID;
select * from customers inner join orders on customers.CustomerID = orders.CustomerID;
select * from customers left join orders on customers.CustomerID = orders.CustomerID;
select * from customers right join orders on customers.CustomerID = orders.CustomerID;
select * from customers full join orders on customers.CustomerID = orders.CustomerID;

--The SQL UNION Operator
select CustomerID from customers union select OrderId from orders;
select City, Country  from customers where Country= 'Germany' union select City, Country from suppliers where Country = 'Germany';
select 'Himanshu' as Mentor, ContactName, City, Country from customers union select 'Supplier', ContactName, City, Country from suppliers;
	
--SQL GROUP BY Statement
select count(CustomerID), Country from customers group by Country;
select count(CustomerID), Country from customers group by Country order by count(CustomerID) DESC;

--SQL HAVING Clause
select count(CustomerID), Country from customers group by Country having count(CustomerID) > 10 order by count(CustomerID) desc;	

--SQL EXISTS Operator
select SupplierName from suppliers where exists (select ProductName from products inner join suppliers on products.SupplierID = suppliers.SupplierID where products.SupplierID = suppliers.SupplierID and Price < 20);
select SupplierName,SupplierID from suppliers where exists (select ProductName from products where products.SupplierID = suppliers.SupplierID and Price < 20);
