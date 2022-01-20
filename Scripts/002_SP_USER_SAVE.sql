use nubimetricsdb
go

drop procedure sp_user_save
go

CREATE procedure sp_user_save(@id int, @nombre varchar(100), @apellido varchar(100), @email varchar(100), @password varchar(100))
as

declare @error varchar(200) = '';

BEGIN TRY

	--validar existencia de mail
	if(exists(select 1 a from "user" where id <> @id and email = @email) )
	begin
		;THROW 50000 ,'Email Existente', 0;
	end;

	if(@id = 0 ) --hay que crear
	begin
		if(isnull(@nombre,'') = '' or  isnull(@apellido,'') = '' or  isnull(@email,'') = '' or  isnull(@password,'') = '')
		begin
			;THROW 50000 ,'Campos faltantes', 0;
		end;

		insert into "user"
		values (@nombre, @apellido, @email,@password);

		set @id = @@Identity;

	end

	else -- hay que actualizar
	begin
		update "user"
		set 
			nombre = case when isnull(@nombre,'') = '' then nombre else @nombre end,
			apellido = case when isnull(@apellido,'') = '' then apellido else @apellido end,
			email = case when isnull(@email,'') = '' then email else @email end,
			password = case when isnull(@password,'') = '' then password else @password end
		where 
			id = @id;

	end 
END TRY
BEGIN CATCH
	set @error = ERROR_MESSAGE()
END CATCH

select @id id, @error error;

go
