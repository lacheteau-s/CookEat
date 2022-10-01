DROP TABLE IF EXISTS schema_version;
CREATE TABLE schema_version
(
	version int NOT NULL,
	update_date DATETIME DEFAULT GETUTCDATE() NOT NULL,
	file_name VARCHAR(64) NOT NULL
);

DROP INDEX IF EXISTS schema_version_index ON schema_version;
CREATE UNIQUE INDEX schema_version_index ON schema_version(version);