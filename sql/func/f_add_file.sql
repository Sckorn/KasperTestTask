USE KasperTestTask
GO  
DROP PROCEDURE KasperTestTask.f_add_file;  
GO 
CREATE PROCEDURE KasperTestTask.f_add_file
    @base_name nvarchar(255),   
    @full_name nvarchar(1024),
	@size				int,
	@value 				int OUTPUT
AS  
	INSERT INTO KasperTestTask.t_file_list (basename, fullname, size)
	VALUES(@base_name, @full_name, @size);

	SET @value = SCOPE_IDENTITY()

GO  