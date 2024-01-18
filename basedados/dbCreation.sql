Use Li4
-- Create the Li4 schema if it doesn't exist
IF (SCHEMA_ID('Li4') IS NULL) 
BEGIN
    EXEC ('CREATE SCHEMA [Li4] AUTHORIZATION [dbo]')
END
-- Drop foreign key constraints before dropping the Utilizador table
IF OBJECT_ID('Li4.fk_Utilizador_Morada', 'F') IS NOT NULL
    ALTER TABLE Li4.Utilizador DROP CONSTRAINT fk_Utilizador_Morada;

-- Drop foreign key constraints before dropping the Leilao table
IF OBJECT_ID('Li4.fk_Leilao_Utilizador1', 'F') IS NOT NULL
    ALTER TABLE Li4.leilao DROP CONSTRAINT fk_Leilao_Utilizador1;

-- Drop foreign key constraints before dropping the utilizador_leilao table
IF OBJECT_ID('Li4.fk_utilizador_leilao_Utilizador1', 'F') IS NOT NULL
    ALTER TABLE Li4.utilizador_leilao DROP CONSTRAINT fk_utilizador_leilao_Utilizador1;

IF OBJECT_ID('Li4.fk_utilizador_leilao_Leilao1', 'F') IS NOT NULL
    ALTER TABLE Li4.utilizador_leilao DROP CONSTRAINT fk_utilizador_leilao_Leilao1;

IF OBJECT_ID('Li4.fk_utilizador_leilao_licitacao1', 'F') IS NOT NULL
    ALTER TABLE Li4.utilizador_leilao DROP CONSTRAINT fk_utilizador_leilao_licitacao1;

-- Drop foreign key constraints before dropping the Quadro table
IF OBJECT_ID('Li4.fk_Quadro_Leilao1', 'F') IS NOT NULL
    ALTER TABLE Li4.Quadro DROP CONSTRAINT fk_Quadro_Leilao1;

-- Drop the tables

-- Table utilizador_leilao
IF OBJECT_ID('Li4.utilizador_leilao', 'U') IS NOT NULL
    DROP TABLE Li4.utilizador_leilao;

-- Table Quadro
IF OBJECT_ID('Li4.Quadro', 'U') IS NOT NULL
    DROP TABLE Li4.Quadro;

-- Table licitacao
IF OBJECT_ID('Li4.licitacao', 'U') IS NOT NULL
    DROP TABLE Li4.licitacao;

-- Table Leilao
IF OBJECT_ID('Li4.leilao', 'U') IS NOT NULL
    DROP TABLE Li4.leilao;

-- Table Utilizador
IF OBJECT_ID('Li4.Utilizador', 'U') IS NOT NULL
    DROP TABLE Li4.Utilizador;

-- Table Morada
IF OBJECT_ID('Li4.Morada', 'U') IS NOT NULL
    DROP TABLE Li4.Morada;


-- Table Morada
CREATE TABLE Li4.Morada (
    idMorada INT IDENTITY(1,1) NOT NULL,
    rua VARCHAR(100) NOT NULL,
    cidade VARCHAR(50) NOT NULL,
    cod_postal VARCHAR(10) NOT NULL,
    pais VARCHAR(45) NOT NULL,
    PRIMARY KEY (idMorada)
);

-- Table Utilizador
CREATE TABLE Li4.Utilizador (
    email VARCHAR(100) NOT NULL,
    nome VARCHAR(45) NOT NULL,
    data_nascimento DATETIME NOT NULL,
    NIF VARCHAR(9) NOT NULL,
	password VARCHAR(65),
    idMorada INT NOT NULL,
    PRIMARY KEY (email),
    CONSTRAINT fk_Utilizador_Morada
        FOREIGN KEY (idMorada)
        REFERENCES Li4.Morada (idMorada)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
)

-- Table Leilao
CREATE TABLE Li4.leilao (
    idLeilao INT IDENTITY(1,1) NOT NULL,
    nome VARCHAR(150) NOT NULL,
    data_inicio DATETIME NOT NULL,
    data_fim DATETIME NOT NULL,
    estado TINYINT NOT NULL,
    valor_base DECIMAL(7,2) NOT NULL,
	pago TINYINT NOT NULL,
    Utilizador_email VARCHAR(100) NOT NULL,
    idQuadro INT NOT NULL,
    PRIMARY KEY (idLeilao),
    CONSTRAINT fk_Leilao_Utilizador1
        FOREIGN KEY (Utilizador_email)
        REFERENCES Li4.Utilizador (email)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);


-- Table licitacao
CREATE TABLE Li4.licitacao (
    idLicitacao INT IDENTITY(1,1) NOT NULL,
    utilizador_email VARCHAR(100) NOT NULL,
    leilao_idLeilao INT NOT NULL,
    data DATETIME NOT NULL,
    valor DECIMAL(12,2) NOT NULL,
    PRIMARY KEY (idLicitacao),
    FOREIGN KEY (utilizador_email) REFERENCES Li4.Utilizador(email),
    FOREIGN KEY (leilao_idLeilao) REFERENCES Li4.leilao(idLeilao)
);


-- Table Quadro
CREATE TABLE Li4.Quadro (
    idQuadro INT IDENTITY(1,1) NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    ano VARCHAR(4) NOT NULL,
    altura DECIMAL(7,2) NOT NULL,
    largura DECIMAL(7,2) NOT NULL,
    descricao NVARCHAR(1000) NOT NULL,
    moldura TINYINT NOT NULL,
    autor VARCHAR(100) NOT NULL,
	imagem VARCHAR(MAX) NOT NULL,
    PRIMARY KEY (idQuadro),
);