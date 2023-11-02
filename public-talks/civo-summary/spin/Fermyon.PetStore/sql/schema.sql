CREATE TABLE pets (
  id integer PRIMARY KEY,
  name varchar(80) not null,
  picture bytea
);

CREATE TABLE toys (
  id integer PRIMARY KEY,
  owner_id integer REFERENCES pets (id),
  count integer,
  description varchar(200) not null,
  picture bytea
);


-- Inserting pets
INSERT INTO pets (id, name) VALUES (1, 'Bella');
INSERT INTO pets (id, name) VALUES (2, 'Charlie');
INSERT INTO pets (id, name) VALUES (3, 'Lucy');
INSERT INTO pets (id, name) VALUES (4, 'Max');
INSERT INTO pets (id, name) VALUES (5, 'Lily');
INSERT INTO pets (id, name) VALUES (6, 'Rocky');
INSERT INTO pets (id, name) VALUES (7, 'Molly');
INSERT INTO pets (id, name) VALUES (8, 'Daisy');
INSERT INTO pets (id, name) VALUES (9, 'Oscar');
INSERT INTO pets (id, name) VALUES (10, 'Buddy');
