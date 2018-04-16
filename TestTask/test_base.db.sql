BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Cards` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`code`	INTEGER NOT NULL UNIQUE,
	`value`	INTEGER NOT NULL,
	`status_realized`	NUMERIC NOT NULL DEFAULT 0,
	`date_create`	TEXT NOT NULL,
	`date_realized`	TEXT
);
COMMIT;
