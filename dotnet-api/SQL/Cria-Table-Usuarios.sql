--drop table Usuarios_Cad
Create table Usuarios_Cad 
(
	PK_Cod_Usuario int IDENTITY(1,1) primary key,
	Usuario varchar(20) not null,
	Senha varchar(40) not null,
	Email varchar(40) not null
)
go
insert into Usuarios_Cad values('teste','123','teste@gmail.com')
