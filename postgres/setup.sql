CREATE ROLE program WITH PASSWORD 'test';
ALTER ROLE program WITH LOGIN;
CREATE DATABASE tickets;
    GRANT ALL PRIVILEGES ON DATABASE tickets TO postgres;

    \c tickets

    CREATE TABLE IF NOT EXISTS ticket
    (
        id            SERIAL PRIMARY KEY,
        ticket_uid     uuid UNIQUE NOT NULL,
        username      VARCHAR(80) NOT NULL,
        flight_number VARCHAR(20) NOT NULL,
        price         INT         NOT NULL,
        status        VARCHAR(20) NOT NULL CHECK (status IN ('PAID', 'CANCELED'))
    );

    GRANT ALL PRIVILEGES ON TABLE ticket TO postgres;
    GRANT ALL PRIVILEGES ON SEQUENCE ticket_id_seq TO postgres;

    CREATE DATABASE flights;
    GRANT ALL PRIVILEGES ON DATABASE flights TO postgres;

    \c flights

    CREATE TABLE IF NOT EXISTS airport
    (
        id      SERIAL PRIMARY KEY,
        name    VARCHAR(255),
        city    VARCHAR(255),
        country VARCHAR(255)
    );

    INSERT INTO airport (name, city, country) values ('Шереметьево', 'Москва', 'Россия');
    INSERT INTO airport (name, city, country) values ('Пулково', 'Санкт-Петербург', 'Россия');

    CREATE TABLE IF NOT EXISTS flight
    (
        id              SERIAL PRIMARY KEY,
        flight_number          VARCHAR(20)              NOT NULL,
        datetime        TIMESTAMP WITH TIME ZONE NOT NULL,
        from_airport_id INT REFERENCES airport (id),
        to_airport_id   INT REFERENCES airport (id),
        price           INT                      NOT NULL
    );

    INSERT INTO flight (flight_number, datetime, from_airport_id, to_airport_id, price)
        values ('AFL031', cast('2021-10-08 20:00:00' as timestamp with time zone), 2, 1, 1500);

    GRANT ALL PRIVILEGES ON TABLE airport TO postgres;
    GRANT ALL PRIVILEGES ON SEQUENCE airport_id_seq TO postgres;
    GRANT ALL PRIVILEGES ON TABLE flight TO postgres;
    GRANT ALL PRIVILEGES ON SEQUENCE flight_id_seq TO postgres;

    CREATE DATABASE privileges;
    GRANT ALL PRIVILEGES ON DATABASE privileges TO postgres;

    \c privileges

    CREATE TABLE IF NOT EXISTS privilege
    (
        id       SERIAL PRIMARY KEY,
        username VARCHAR(80) NOT NULL UNIQUE,
        status   VARCHAR(80) NOT NULL DEFAULT 'BRONZE' CHECK (status IN ('BRONZE', 'SILVER', 'GOLD')),
        balance  INT
    );

    CREATE TABLE IF NOT EXISTS privilege_history
    (
        id             SERIAL PRIMARY KEY,
        privilege_id   INT REFERENCES privilege (id),
        ticket_uid     uuid        NOT NULL,
        datetime       TIMESTAMP   NOT NULL,
        balance_diff   INT         NOT NULL,
        operation_type VARCHAR(20) NOT NULL CHECK (operation_type IN ('FILL_IN_BALANCE', 'DEBIT_THE_ACCOUNT'))
    );

    INSERT INTO privilege (id, username, status, balance) VALUES (1, 'Test Max', 'BRONZE', 0);

    GRANT ALL PRIVILEGES ON TABLE privilege TO postgres;
    GRANT ALL PRIVILEGES ON SEQUENCE privilege_id_seq TO postgres;
    GRANT ALL PRIVILEGES ON TABLE privilege_history TO postgres;
    GRANT ALL PRIVILEGES ON SEQUENCE privilege_history_id_seq TO postgres;