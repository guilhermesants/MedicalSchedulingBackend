-- perfis
CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

-- usuarios
CREATE TABLE users (
    id BIGSERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    last_login TIMESTAMP,
    active BOOLEAN NOT NULL DEFAULT true,,
    role_id INT NOT NULL REFERENCES roles(id)
);

-- medicos
CREATE TABLE doctors (
    id BIGSERIAL PRIMARY KEY,
    user_id BIGINT UNIQUE REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(150) NOT NULL,
    license_number VARCHAR(20) UNIQUE NOT NULL,
    specialty VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100) UNIQUE
);

-- pacientes
CREATE TABLE patients (
    id BIGSERIAL PRIMARY KEY,
    user_id BIGINT UNIQUE REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(150) NOT NULL,
    cpf VARCHAR(14) UNIQUE NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100) UNIQUE
);

-- disponibilitade medico
CREATE TABLE availabilities (
    id BIGSERIAL PRIMARY KEY,
    doctor_id BIGINT NOT NULL REFERENCES doctors(id) ON DELETE CASCADE,
    date DATE NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
	available BOOLEAN NOT NULL DEFAULT true,
    CONSTRAINT ck_time CHECK (start_time < end_time),
    CONSTRAINT uq_doctor_date UNIQUE (doctor_id, date, start_time, end_time)
);

-- status agendamento
CREATE TABLE appointment_status (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

-- Agendamentos
CREATE TABLE appointments (
    id BIGSERIAL PRIMARY KEY,
    patient_id BIGINT NOT NULL REFERENCES patients(id) ON DELETE CASCADE,
	availability_id BIGINT NOT NULL REFERENCES availabilities(id) ON DELETE CASCADE,
    time TIME NOT NULL,
    status_id INT NOT NULL REFERENCES appointment_status(id)
);

CREATE UNIQUE INDEX uq_active_appointment
ON appointments (availability_id, time)
where status_id = 1;

INSERT INTO appointment_status (name) VALUES
('active'),
('canceled'),
('completed');

INSERT INTO roles (name) VALUES
('admin'),
('doctor'),
('patient');

INSERT INTO users
(username, password, last_login, active, role_id) VALUES
('admin', '$2y$10$rEelBQYfVeir2VXt1sIOjOOv67Omxw5HU63ook5R0Hfqoddc8olUe', null, true, 1)