-- Chú ý: Hãy copy và chạy các script này trong công cụ quản lý cơ sở dữ liệu MySQL của bạn (VD: MySQL Workbench, DBeaver, Heidisql)

USE `misaweb06`; -- (Thay đổi tên DB này nếu tên CSDL của bạn trỏ vào khác)

DELIMITER $$
CREATE PROCEDURE `Proc_employees_GetAll`()
BEGIN
	SELECT 
		employee_id, 
        employee_name, 
        employee_age 
	FROM employees;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Proc_employees_GetById`(
	IN p_employee_id CHAR(36)
)
BEGIN
	SELECT 
		employee_id, 
        employee_name, 
        employee_age 
	FROM employees 
    WHERE employee_id = p_employee_id LIMIT 1;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Proc_employees_Insert`(
	IN p_EmployeeId CHAR(36),
    IN p_EmployeeName VARCHAR(255),
    IN p_EmployeeAge INT
)
BEGIN
	INSERT INTO employees(employee_id, employee_name, employee_age)
    VALUES (p_EmployeeId, p_EmployeeName, p_EmployeeAge);
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Proc_employees_Update`(
	IN p_EmployeeId CHAR(36),
    IN p_EmployeeName VARCHAR(255),
    IN p_EmployeeAge INT
)
BEGIN
	UPDATE employees
    SET 
		employee_name = p_EmployeeName,
        employee_age = p_EmployeeAge
	WHERE 
		employee_id = p_EmployeeId;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Proc_employees_Delete`(
	IN p_employee_id CHAR(36)
)
BEGIN
	DELETE FROM employees 
    WHERE employee_id = p_employee_id;
END$$
DELIMITER ;
