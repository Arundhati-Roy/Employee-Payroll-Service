/*UC1*/
use tsetDB
drop table employee_payroll
/*UC2*/
create table employee_payroll(

id int NOT NULL identity(1,1) PRIMARY KEY,
name VARCHAR(50) NOT NULL,
salary DECIMAL(10,2) NOT NULL,
startDate DATETIME NOT NULL ) ;


 /*UC3*/
 insert into employee_payroll(name,salary,startDate)
 values('AR',67897,'2019-08-30'),
 ('NR',6757698,'2018-05-23'),
 ('PR',8786758,'2012-09-03'),
 ('SR',236758,'1987-12-13')
 ;
 --Doubt: When I try to add values but it fails, id still gets incremented'''

 /*UC4*/
SELECT * FROM employee_payroll

 exec sp_help employee_payroll

 /*UC5*/
SELECT * FROM employee_payroll
where name='AR'

SELECT * FROM employee_payroll
WHERE startDate BETWEEN CAST('2019-09-22'
AS DATE) AND getDATE();

 /*UC6*/
 Alter table employee_payroll
 Add gender varchar(30)
UPDATE employee_payroll set gender ='F' 
where name = 'AR' or 
name ='Charlie';
SELECT * FROM employee_payroll

UPDATE employee_payroll set basicPay=30000
where id in (select id from employee where name='AR')

 /*UC7*/
SELECT SUM(salary) as SalF FROM employee_payroll
WHERE gender = 'F' GROUP BY gender;

 /*UC8*/
 Alter table employee_payroll
 drop column empPhone,addr,dept;

  Alter table employee_payroll
 Add empPhone varchar(30),
 addr varchar(255) Default 'Mumbai',
 dept varchar(30) Default 'NA'
WITH VALUES ;
 SELECT * FROM employee_payroll


  /*UC9*/
 Alter table employee_payroll
 drop column basicPay,deductions,taxablePay,incomeTax,NetPay;
 Alter table employee_payroll
 Add basicPay int,
 deductions int,
 taxablePay int,
 incomeTax int,
 NetPay as basicPay-deductions-taxablePay-incomeTax;
 SELECT * FROM employee_payroll

 /*UC10*/
 insert into employee_payroll(name,salary,startDate,gender,empPhone,addr,
 dept,basicPay,deductions,taxablePay,incomeTax)
 values('Teressia',5667897,'2017-08-20','F',876556789,'Pune','Sales',65857,678,234,5876);
  SELECT * FROM employee_payroll

/*-------------------------------------*/
  /*UC11_7*/
drop table payroll
drop table empDept
drop table employee
drop table department

create table employee(
empId int NOT NULL identity(1,1) PRIMARY KEY,
name VARCHAR(50) NOT NULL
 ) ;

create table department(
deptId varchar(15) not null PRIMARY KEY,
deptName VARCHAR(50) NOT NULL
 ) ;

create table empDept(
empId int NOT NULL,
deptId varchar(15) not null,
constraint FK_empDept_deptID foreign key (deptId)
REFERENCES department(deptId),
constraint FK_empDept_empID foreign key (empId)
REFERENCES employee(empId),
primary key(empId,deptId)
 ) ;

create table payroll(
salId int not null identity(1,1) PRIMARY KEY,
empId int not null,
startDate DATETIME NOT NULL,
basicPay DECIMAL(10,2) NOT NULL,
deductions as 0.2*basicPay,
taxablePay as basicPay-(0.2*basicPay),
incomeTax as 0.1*(basicPay-(0.2*basicPay)),
NetPay as basicPay- (0.1*(basicPay-(0.2*basicPay))),
constraint FK_payroll_empId foreign key (empId)
REFERENCES employee(empId)
 ) ;

Alter table payroll
add deductions as 0.2*basicPay
Alter table payroll
add taxablePay as basicPay-deductions
Alter table payroll
add incomeTax as 0.1*taxablePay
Alter table payroll
add NetPay as basicPay-incomeTax
;

 select * from employee;
 select * from department;
 select * from empDept;
 select * from payroll

 /*UC3*/
 insert into department(deptId,deptName)
 values('S1','Sales'),
 ('M2','Marketing'),
 ('F3','Finance'),
 ('O7','Operating');

 /*insert into employee(name)
 values('S1','AR'),
 ('M2','NR'),
 ('F3','PR'),
 ('O7','SR')
 ;
 */

 insert into employee(name)
 values('AR'),
 ('NR'),
 ('PR'),
 ('SR');

 insert into empDept
 values(1,'S1'),
 (2,'M2'),
 (3,'F3'),
 (4,'O7');

 insert into payroll
values (1,'2018-04-02',658769.00),
(2,'2017-09-22',658769.00),
(3,'2019-05-02',126769.00),
(4,'2010-07-14',36769.00);

 insert into employee(name)
 values('Bec'),
 ('Sam'),
 ('KP'),
 ('Aru');


 select * from empDept ed inner join department d 
 on ed.deptId=d.deptId
 inner join employee e on e.empId=ed.empId

 --Doubt: When I try to add values but it fails, id still gets incremented

 /*UC4*/
SELECT * FROM employee
 exec sp_help employee

 SELECT e.empId FROM employee e, payroll p
WHERE startDate BETWEEN CAST('2010-09-22' AS DATE) AND getDATE()
and e.empId=p.empId;


 /*UC6*/
 Alter table employee
 Add gender varchar(30)
UPDATE employee set gender ='F' 
where name = 'Neha' or 
name ='Shreya';
SELECT * FROM employee

 /*UC7*/
SELECT SUM(basicPay) as SumF,Avg(basicPay) as AvgF,
Max(basicPay) as MaxF, min(basicPay) as MinF
FROM payroll 

where gender='F'
group by gender


 /*UC8*/
 Alter table employee
 drop column empPhone,addr;

 Alter table employee
 Add empPhone varchar(30),
 addr varchar(255) Default 'Mumbai'
 WITH VALUES ;
 SELECT * FROM employee

 select * from empDept ed 
 inner join department d on ed.deptId=d.deptId
 inner join employee e on e.empId=ed.empId
 inner join payroll p on p.empId=e.empId

SELECT * FROM employee
SELECT * FROM payroll
select * from department
select * from empDept

 UPDATE payroll set basicPay=30000.00
where empId 
--=5 or empId=9
in (select empID from employee where name='AR')

delete from employee
where empId not in (1,2,4)
delete from department
where deptId='S4'
insert into empDept
values(11,'S1')


select distinct * from employee e,department d,payroll p,empDept ed
where ed.deptId=d.deptId and e.empId=p.empId and ed.empId=e.empId
--and e.name='NR'

--********************************
--Stored Procedure
set ansi_nulls on
go
set quoted_identifier on
go

create procedure uPayroll
--Add parameters for stored procedure
	@basicPay decimal(10,2),
	@name varchar(20)

as
begin
set xact_abort on;
begin try
begin transaction;
	UPDATE payroll set basicPay=@basicPay
	where empId in (select empId from employee where name=@name)
	--To retieve the updated salary
	select * from payroll;
	commit transaction
end try

begin catch
select ERROR_NUMBER() as errorNum, 
ERROR_MESSAGE() as ErrorMess;
if(XACT_STATE()=-1)
	begin
		print N'The transaction is in an uncommittable state.' + 'Rolling back transaction.'
		Rollback transaction ;
	end;
if(XACT_STATE()=1)
	begin
		print N'The transaction is committable state.' + 'Commiting transaction.'
		Commit transaction ;
	end;
end catch
end
go;

exec uPayroll @basicPay=35000.00, @name='AR'

--******************************************************
--Procedure to consequently add details while adding Employee
drop procedure spAddEmployee

create procedure spAddEmployee
@name VARCHAR(20),
@basicPay DECIMAL(10,2),
@start_Date DATETIME,
@deptName varchar(20)

as
begin
set xact_abort on;
begin try
	begin transaction
	insert into employee(name) values(@name)
	declare @id int
	set @id=(select max(empId) from employee)
	--insert into department values(@deptId,@deptName)
	declare @Did varchar(10)
	set @Did=(select deptId from department where deptName=@deptName)
	insert into empDept values(@id,@Did)
	insert into payroll 
	values(@id,@start_Date,@basicPay)
	--To retieve the updated salary
	select * from employee e,department d,payroll p,empDept ed
	where ed.deptId=d.deptId and e.empId=p.empId and ed.empId=e.empId
	commit transaction
end try
begin catch
select ERROR_NUMBER() as errorNum, 
ERROR_MESSAGE() as ErrorMess;
if(XACT_STATE()=-1)
	begin
		print N'The transaction is in an uncommittable state.' + 'Rolling back transaction.'
		Rollback transaction ;
	end;
if(XACT_STATE()=1)
	begin
		print N'The transaction is committable state.' + 'Commiting transaction.'
		Commit transaction ;
	end;
end catch
end
go

exec spAddEmployee @basicPay=35000.00, @name='PR',@start_date='2015-03-14',@deptName='Sales'
