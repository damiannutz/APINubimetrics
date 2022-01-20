use nubimetricsdb
go

create table "user"(
	id int primary key identity,
	nombre varchar(100) not null,
	apellido varchar(100) not null,
	email varchar(100) unique not null,
	password varchar(100) not null
)
go


