use nubimetricsdb
go

CREATE procedure sp_user_delete(@id int)
as

DELETE "user" where id = @id;

go
