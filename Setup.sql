-- USE petshop226;

-- CREATE TABLE users
-- (
--     id VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL UNIQUE,
--     name VARCHAR(255) NOT NULL,
--     passwordHash VARCHAR(255) NOT NULL, 
--     rate DECIMAL(5, 2) NOT NULL,
--     skills VARCHAR(255),

--     INDEX(email),

--     PRIMARY KEY (id)
-- );


-- INSERT INTO users (id, email, name, passwordHash, rate, skills)
-- VALUES ("hello-17", "j@j.com", "Jake", "shhhh", 170.99, "Numchuck Skills, Bo Staff Skills");

SELECT * FROM users WHERE email = "j@j.com";

-- INSERT INTO contractors (id, name, rate)
-- VALUES ("hello-17", "Bob the Builder", 1234567.23);

-- SELECT * FROM contractors WHERE id = "hello-17";

-- CREATE TABLE jobs
-- (
--     id INT AUTO_INCREMENT,
--     title VARCHAR(255),
--     location VARCHAR(255),
--     budget DECIMAL(10, 2),
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE contractorJobs (
--     id int AUTO_INCREMENT,
--     contractorId VARCHAR(255) NOT NULL,
--     jobId INT NOT NULL,
--     PRIMARY KEY (id),
--     INDEX (jobId),

--     FOREIGN KEY (contractorId) 
--         REFERENCES contractors(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (jobId)
--         REFERENCES jobs(id)
--         ON DELETE CASCADE
-- )


-- INSERT INTO contractors ( id, name, rate)
--     VALUES ("3h234n", "DMoney", 100.50),
--             ("23lj34", "Jake", 25.88),
--             ("23lkj3", "Mark", 3.00);

-- INSERT INTO jobs (title, location, budget)
--     VALUES ("BCW Expansion", "123 Capital Blv", 100000.4),
--             ("BOA", "1234 Fake St", 1000),
--             ("Jamies Burger Den", "568 Poison ave", 100);

-- INSERT INTO contractorjobs (contractorId, jobId)
--     VALUES ("3h234n", 1), ("3h234n", 2), ("3h234n", 3), ("23lj34", 1), ("23lj34", 2), ("23lkj3", 3);

-- SELECT * FROM contractorjobs

-- DELETE FROM contractorJobs
--     WHERE jobId = 3 AND contractorId = "23lkj3";

-- DELETE FROM contractors
--     WHERE id = "23lj34";

-- SELECT * FROM contractorjobs cj
--     INNER JOIN contractors c ON c.id = cj.contractorId
--     WHERE jobId = 1;