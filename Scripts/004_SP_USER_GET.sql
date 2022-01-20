use nubimetricsdb
go

CREATE procedure sp_user_get(@id int,  @nombre varchar(100), @apellido varchar(100), @email varchar(100))
as

select * from "user" 
where 
		id = case when isnull(@id,0) = 0 then id else @id end and
		nombre = case when isnull(@nombre,'') = '' then nombre else @nombre end and
		apellido = case when isnull(@apellido,'') = '' then apellido else @apellido end and
		email = case when isnull(@email,'') = '' then email else @email end
	;
go
