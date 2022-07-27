--drop table Users
Create table Users 
(
	id_user int IDENTITY(1,1) primary key,
	user_login varchar(20) not null,
	password varchar(40) not null,
	email varchar(40) not null
)
go
insert into Users values('teste','123','teste@gmail.com')
