-- Active: 1703085620939@@34.213.246.130@3306@keepr

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User Cover Image',
        website varchar(255) COMMENT 'User website',
        github varchar(255) COMMENT 'User github',
        linkedin varchar(255) COMMENT 'User linkedin',
        resume varchar(255) COMMENT 'User resume',
        bio varchar(255) COMMENT 'User bio'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name CHAR(100) NOT NULL,
        description VARCHAR(1000) NOT NULL,
        img VARCHAR(300) NOT NULL,
        isPrivate BOOLEAN NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name CHAR(100) NOT NULL,
        description VARCHAR(1000) NOT NULL,
        img VARCHAR(300) NOT NULL,
        views INT UNSIGNED NOT NULL DEFAULT 0,
        kept INT UNSIGNED NOT NULL DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        UNIQUE (vaultId, keepId)
    ) default charset utf8 COMMENT '';

SELECT v.*, acc.*
FROM vaults v
    JOIN accounts acc ON acc.id = v.creatorId
WHERE
    isPrivate = false
    OR (
        v.creatorId = "65311cd2974eb0d9a0dbcb7e"
        AND isPrivate = true
    );