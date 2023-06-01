create database Employees_DB;

create table EMPLOYEES(
	Employee_ID BIGINT IDENTITY(1,1) primary key,
	First_Name VARCHAR(255),
	Last_Name VARCHAR(255),
	Email VARCHAR(255) UNIQUE,
	Phone_Number NUMERIC(11,0) UNIQUE ,
	Hire_Date date,
	Job_ID INT FOREIGN KEY REFERENCES JOBS(Job_ID),
	Salary NUMERIC(10, 2),
	Commission_pct NUMERIC(5, 2),
	Manager_ID BIGINT,
	Department_ID TINYINT
);

create table DEPARTMENTS(
	Department_ID INT IDENTITY(1,1) primary key,
	Department_Name VARCHAR(255),
	Manager_ID BIGINT,
	Location_ID BIGINT
);

create table JOBS (
	Job_ID INT IDENTITY(1,1) primary key,
	Job_title VARCHAR(255),
	Min_Salary NUMERIC(20,2),
	Max_Salary NUMERIC(20,2)
);

----------------------------------------------------------
insert into JOBS(Job_title, Min_Salary, Max_Salary)
values ('Front_End_Devloper',30000,200000),
		('Back_End_Devloper',50000,750000),
		('HR',20000,150000),
		('Manager',70000,250000),
		('Salesmen',30000,400000),
		('QA',45000,450000),
		('Intern', 0, 10000);

insert into DEPARTMENTS(Department_Name, Manager_ID, Location_ID)
values ('Front_End',6,1),
	('Back_End',5,2),
	('HR',4,3),
	('Sales', 8,4),
	('QA',20,5);

insert into EMPLOYEES (First_Name, Last_Name, Email, Phone_Number, Hire_Date, Job_ID, Salary, Commission_pct, Manager_ID, Department_ID)
values('Himanshu', 'Ladva', 'Himmanshu@gmail.com', 9510685398, '2021-06-15', 7, 8000, 0.4, 5, 2),
	('Yash', 'Savaliya', 'Yash@gmail.com', 9875412362, '2022-06-15', 7, 8000, 0.8, 4, 2),
	('Yash', 'Nathvani', 'Nathvani@gmail.com',9409249079,'2021-06-16', 2, 8000, 0.7, 6, 1),
	('Pragnesh', 'Bhai', 'Pragensh@gmail.com', 9876598765, '2020-03-29', 7, 190000, 2.9, 5, 2),
	('Priyank', 'Ranapara', 'Priyank@gmail.com', 9409249082, '2022-02-16', 7, 40000, 1.3, 6, 1),
	('Sanjuli', 'Maam', 'Sanjuli@gmail.com', 9409249080, '2022-06-16', 1, 9500, 0.3, 6, 1),
	('Shery', 'Dadhaniya', 'Shrey@gmail.com', 9409249098, '2019-06-16', 1, 125000, 2.2, 5, 2),
	('Bhargav', 'Gadhiya', 'Bhargav@gmail.com', 1237418529, '2019-03-01', 4, 180000, 2.3, 20, 3),
	('Viren', 'Bhai', 'Viren@gmail.com', 7418529630, '2020-09-30', 6, 56000, 1.2, 4, 3),
	('Neha', 'Gandhi', 'Neha@gmail.com', 1237894654, '2020-06-16', 1, 60000, 1.4, 8, 4);


--1
select last_name, job_id, salary AS Sal 
from employees;  --true;

--2
--false
select employee_id, last_name, sal*12 ANNUAL SALARY from EMPLOYEES;
--true
select employee_id, last_name, salary*12 AS SALARY from EMPLOYEES;
--error is syntax near 'SALARY'

--3
EXEC sp_help 'dbo.DEPARTMENTS';

--4
select DISTINCT Job_ID 
from EMPLOYEES;

--5
select Employee_ID, Salary AS OLDsALARY, (Salary+(Salary*0.155)) AS NewSalary, (Salary*0.155) AS Increment 
from EMPLOYEES;

--6
select MAX(Salary) AS Max_Salary, MIN(Salary) AS Min_Salary, SUM(Salary) AS Sum_Of_Salary, AVG(Salary) AS Avg_Salary 
from EMPLOYEES inner join JOBS on JOBS.Job_ID = EMPLOYEES.Job_ID group by EMPLOYEES.Job_ID;

--7
select CONCAT(A.First_Name, ' ', A.Last_Name) AS Name, A.Hire_Date AS Hire_Date, CONCAT(B.First_Name, ' ', B.Last_Name) AS Manager, B.Hire_Date AS Manager_Hire_Date
from EMPLOYEES A INNER JOIN EMPLOYEES B ON A.Manager_ID = B.Employee_ID
where A.Hire_Date < B.Hire_Date;

--8
create view Employee_Report AS
select distinct CONCAT(A.First_Name, ' ', A.Last_Name) AS Emp_Name, A.Department_ID AS Same_Dept, CONCAT(B.First_Name, ' ', B.Last_Name) AS Other_Emp
from EMPLOYEES A INNER JOIN EMPLOYEES B ON A.Department_ID = B.Department_ID
where B.Last_Name != A.Last_Name;

select * from Employee_Report;

--9
select ROUND(MAX(Salary),0) AS Maximum, ROUND(MIN(Salary), 0) AS Minimum, ROUND(SUM(Salary), 0) AS Sum, ROUND(AVG(Salary), 0) AS Average from EMPLOYEES;

--10
create VIEW Salary_List_Report AS
select * from EMPLOYEES 
where Salary > (select Salary from EMPLOYEES where Department_ID = 4);

select * from Salary_List_Report;

--11
create VIEW Employee_To_Shrey AS
select A.Last_Name, A.Salary 
from EMPLOYEES A INNER JOIN EMPLOYEES B ON A.Manager_ID = B.Employee_ID where B.First_Name = 'Shery';

select * from Employee_To_Shrey;	

--12
UPDATE JOBS 
SET Job_title = 'ST_Clerk' where Job_ID = 8;

select Department_ID from EMPLOYEES
EXCEPT
select Department_ID from EMPLOYEES
where Department_ID = 8;

--13
--select Employee_ID, Job_ID, Department_ID from EMPLOYEES where Department_ID IN (1, 3, 4) ORDER BY Department_ID;
select Employee_ID, Job_ID, Department_ID from EMPLOYEES 
where Department_ID = 1 
UNION
select Employee_ID, Job_ID, Department_ID from EMPLOYEES where Department_ID = 3;
