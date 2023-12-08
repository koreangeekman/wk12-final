CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        website varchar(255) COMMENT 'User website',
        github varchar(255) COMMENT 'User github',
        linkedin varchar(255) COMMENT 'User linkedin',
        resume varchar(255) COMMENT 'User resume',
        bio varchar(255) COMMENT 'User bio'
    ) default charset utf8 COMMENT '';