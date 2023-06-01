--create new database
create database Task1;

--create author table
create table author (
  Author_ID varchar(10) not null primary key,
  Author_Name varchar(50) not null
);

--create books table
create table books (
  Book_ID varchar(20) not null primary key,
  Book_Name varchar(200) not null, 
  Author_ID varchar(10) foreign key references author(Author_ID),
  Price int not null,
  Publisher_ID varchar(20) foreign key references publisher(Publisher_ID)
);
drop table books;

--create customer table
create table customer (
  Customer_ID varchar(20) not null primary key,
  Customer_Name varchar(200) not null,
  Street_Address varchar(250) not null,
  City varchar(50) not null,
  Phone_Number varchar(20) not null,
  Credit_Card_Number varchar(100) foreign key references Credit_card_details(Credit_Card_Number)
);

--create credit card details table
create table Credit_card_details (
   Credit_Card_Number varchar(100) not null primary key,
   Credit_Card_Type varchar(70) not null,
   Expiry_Date date not null
);

--create order_details table
create table order_details (
   Order_ID int not null primary key,
   Customer_ID varchar(20) foreign key references customer(Customer_ID),
   shipping_type varchar(50) foreign key references shipping_type(shipping_type),
   Date_of_purchase date not null,
   shopping_cart_ID int foreign key references shopping_cart(shopping_cart_ID),
);

--create publisher table
create table publisher (
   Publisher_ID varchar(20) not null primary key,
   Publisher_Name varchar(200) not null,
);

--create purchase history table 
create table purchase_history (
   Customer_ID varchar(20) foreign key references customer(Customer_ID),
   Order_ID int foreign key references  order_details(Order_ID)
);

--create shipping type table
create table shipping_type (
   shipping_type varchar(50) not null primary key,
   shipping_price int not null
);

--create shopping cart table
create table shopping_cart (
   shopping_cart_ID int not null primary key,
   Book_ID varchar(20) foreign key references books(Book_ID),
   Price int not null, 
   Date date not null,
   Quantity int not null
);

--table 1 author
insert into author (Author_ID, Author_Name) values (1, 'himanshu');
insert into author (Author_ID, Author_Name) values (2, 'yash');
select * from author;

--table 2 books
insert into books (Book_ID, Book_Name, Author_ID, Price, Publisher_ID) values (1, '30 taughts', 1, 200, 1);
insert into books (Book_ID, Book_Name, Author_ID, Price, Publisher_ID) values (2, 'Stree free life', 2, 400, 2);
select * from books;

--table 3 publisher
insert into publisher (Publisher_ID, Publisher_Name) values (1, 'FFF');
insert into publisher (Publisher_ID, Publisher_Name) values (2, 'GGG');
select * from publisher;

--table 4 Credit card details
insert into Credit_card_details (Credit_Card_Number, Credit_Card_Type, Expiry_Date) values ('#123$', 'Rupay', '29-JUN-04');
insert into Credit_card_details (Credit_Card_Number, Credit_Card_Type, Expiry_Date) values ('#354$', 'visa', '10-DEC-05');
select * from Credit_card_details;

--table 5 customer 
insert into customer (Customer_ID, Customer_Name, Street_Address, City, Phone_Number, Credit_Card_Number) values (
  1,'universal king', 'shapar-veraval', 'Rajkot', '9510685398', '#123$' 
);
insert into customer (Customer_ID, Customer_Name, Street_Address, City, Phone_Number, Credit_Card_Number) values (
  2,'legengd killer', 'los-angeles', 'varsegama', '1234455677', '#354$' 
);

--table 6 shopping cart
insert into shopping_cart (shopping_cart_ID, Book_ID, Price, Date, Quantity) values (1,1, 350, '31-JAN-07', 4);
insert into shopping_cart (shopping_cart_ID, Book_ID, Price, Date, Quantity) values (2,2, 500, '28-FEB-09', 5);
select * from shopping_cart;

--table 7 shipping_type 
insert into shipping_type (shipping_type, shipping_price) values ('Home delivery', 150);
insert into shipping_type (shipping_type, shipping_price) values ('Center delivery', 200);
select * from shipping_type;

--table 8 order details
insert into order_details (Order_ID, Customer_ID, shipping_type,Date_of_purchase, shopping_cart_ID) values (
  1, 1, 'Home delivery', '27-SEP-12', 1
);
insert into order_details (Order_ID, Customer_ID, shipping_type,Date_of_purchase, shopping_cart_ID) values (
  2, 2, 'Center delivery', '18-OCT-14', 2
);
select * from order_details;

--table 9 purchase_history
insert into purchase_history (Customer_ID, Order_ID) values (1, 1);
insert into purchase_history (Customer_ID, Order_ID) values (2, 2);
select * from purchase_history;

--create view of the database
create view my_task1 as select Customer_Name, Street_Address from customer;
create view my_task_view2 as select Order_ID, Customer_ID, shipping_type, Date_of_purchase, shopping_cart_ID from order_details;