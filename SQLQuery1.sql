CREATE TABLE puntoret (
    id VARCHAR(10) PRIMARY KEY,
    emri VARCHAR(20) NOT NULL,
    contact_info VARCHAR(150),
    data_punsimit DATE NOT NULL
);

CREATE TABLE p_oraret (
    id VARCHAR(10) PRIMARY KEY,
    puntoret_id VARCHAR(10),
    data_ndrimit DATE NOT NULL,
    fillimi_ndrimit TIME NOT NULL,
    mbarimi_ndrimit TIME NOT NULL,
    zgjatja_ndrimit DECIMAL(4,2),
    FOREIGN KEY (puntoret_id) REFERENCES puntoret(id)
);

ALTER TABLE p_oraret
ADD CONSTRAINT FK_p_oraret_puntoret
FOREIGN KEY (puntoret_id) REFERENCES puntoret(id) ON DELETE CASCADE;

CREATE TABLE produkte (
    id VARCHAR(10) PRIMARY KEY,
    Emri VARCHAR(20) NOT NULL,
    kategoria VARCHAR(50),
    cmimi_njesi DECIMAL(10,2) NOT NULL
);
ALTER TABLE shitje_artikuj
ADD CONSTRAINT FK_shitje_artikuj_produkte
FOREIGN KEY (produkte_id) REFERENCES produkte(id) ON DELETE CASCADE;

CREATE TABLE shitjet (
    id VARCHAR(10) PRIMARY KEY,
    datakoha_shitjes DATETIME NOT NULL,
    cmimi_total DECIMAL(10,2) NOT NULL,
    puntoret_id VARCHAR(10),
    FOREIGN KEY (puntoret_id) REFERENCES puntoret(id)
);
ALTER TABLE shitjet
ADD CONSTRAINT FK_shitjet_puntoret
FOREIGN KEY (puntoret_id) REFERENCES puntoret(id) ON DELETE CASCADE;

CREATE TABLE shitje_artikuj (
    id VARCHAR(10) PRIMARY KEY,
    shitjet_id VARCHAR(10),
    produkte_id VARCHAR(10),
    sasia INT NOT NULL,
    cmimi_njesi DECIMAL(10,2) NOT NULL,
    cmimi_total DECIMAL(10,2),
    FOREIGN KEY (shitjet_id) REFERENCES shitjet(id),
    FOREIGN KEY (produkte_id) REFERENCES produkte(id)
);
ALTER TABLE shitje_artikuj
ADD CONSTRAINT FK_shitje_artikuj_shitjet
FOREIGN KEY (shitjet_id) REFERENCES shitjet(id) ON DELETE CASCADE;

CREATE TABLE ardhurat_ditore (
    id VARCHAR(10) PRIMARY KEY,
    data_ardhurat DATE NOT NULL UNIQUE,
    ardhurat_total DECIMAL(10,2)
);

CREATE TABLE hargjimet (
    id VARCHAR(10) PRIMARY KEY,
    data_hargjimeve DATE NOT NULL,
    kategoria VARCHAR(50),
    pershkrimi TEXT,
    sasia DECIMAL(10,2)
);

-- insertimi neper tabela
INSERT INTO puntoret VALUES ('123', 'Amar Ajvazi', 'Amarbosi@gmail.com', '2025-03-01');
INSERT INTO puntoret VALUES ('234', 'Abdulhamind Alimi', 'Messi10@gmail.com', '2025-01-15');
INSERT INTO puntoret VALUES ('345', 'Aga Fadil', 'Fadilmajstori@gmail.com', '2025-02-01');

INSERT INTO produkte VALUES ('1', 'Espresso', 'Pije', 90);
INSERT INTO produkte VALUES ('2', 'Cappuccino', 'Pije', 110);
INSERT INTO produkte VALUES ('3', 'Sandwich', 'Ushqim', 150);

INSERT INTO p_oraret VALUES ('1', '123', '2025-05-20', '08:00:00', '14:00:00', 6.0);
INSERT INTO p_oraret VALUES ('2', '234', '2025-05-20', '14:00:00', '20:00:00', 6.0);
INSERT INTO p_oraret VALUES ('3', '345', '2025-05-21', '08:00:00', '14:00:00', 6.0);

INSERT INTO shitjet VALUES ('1', '2024-05-20 09:00:00', 350, '123');
INSERT INTO shitjet VALUES ('2', '2024-05-20 10:00:00', 180, '123');
INSERT INTO shitjet VALUES ('3', '2024-05-21 12:00:00', 300, '123');

INSERT INTO shitje_artikuj VALUES ('1', '1', '1', 1, 250, 250);
INSERT INTO shitje_artikuj VALUES ('2', '1', '2', 1, 300, 300);
INSERT INTO shitje_artikuj VALUES ('3', '2', '2', 1, 300, 300);

INSERT INTO ardhurat_ditore VALUES ('1', '2025-05-20', 850);
INSERT INTO ardhurat_ditore VALUES ('2', '2025-05-21', 500);
INSERT INTO ardhurat_ditore VALUES ('3', '2025-05-22', 0.00);

INSERT INTO hargjimet VALUES ('1', '2025-05-20', 'Furnizime', 'blerje tkafes', 1200);
INSERT INTO hargjimet VALUES ('2', '2025-05-20', 'Hargjime tjera', 'rryma', 3000);
INSERT INTO hargjimet VALUES ('3', '2025-05-21', 'Mirmbajtje', 'Rregullim i aparatit te kafes', 6000);

-- 3 Update
UPDATE puntoret SET contact_info = 'Amar007@gmail.com' WHERE id = '123';
UPDATE produkte SET cmimi_njesi = 200 WHERE id = '3';
UPDATE shitjet SET cmimi_total = 600 WHERE id = '3';

-- 3 DELETE 
DELETE FROM shitje_artikuj WHERE id = '3';
DELETE FROM p_oraret WHERE id = '2';
DELETE FROM hargjimet WHERE id = '1';
DELETE FROM puntoret WHERE id = '456';

-- DML
SELECT puntoret_id, COUNT(*) AS shitjet_total FROM shitjet GROUP BY puntoret_id;
SELECT shitjet_id, SUM(sasia * cmimi_njesi) AS totali_llogaritur FROM shitje_artikuj GROUP BY shitjet_id;
SELECT data_hargjimeve, SUM(sasia) AS hargjimet_total FROM hargjimet GROUP BY data_hargjimeve;

SELECT 
    p.id AS punetori_id,
    p.emri,
    o.data_ndrimit,
    o.fillimi_ndrimit,
    o.mbarimi_ndrimit,
    o.zgjatja_ndrimit
FROM puntoret p
JOIN p_oraret o ON p.id = o.puntoret_id;

GO

CREATE PROCEDURE TotaliShitjevePerPunetor
    @punetoriID VARCHAR(10)
AS
BEGIN
    SELECT 
        puntoret_id,
        COUNT(*) AS numri_shitjeve,
        SUM(cmimi_total) AS totali_shitjeve
    FROM shitjet
    WHERE puntoret_id = @punetoriID
    GROUP BY puntoret_id;
END;

EXEC TotaliShitjevePerPunetor @punetoriID = '123';

GO

CREATE PROCEDURE ShtoPunetor
    @id VARCHAR(10),
    @emri VARCHAR(20),
    @contact_info VARCHAR(150),
    @data_punsimit DATE
AS
BEGIN
    INSERT INTO puntoret (id, emri, contact_info, data_punsimit)
    VALUES (@id, @emri, @contact_info, @data_punsimit);
END;

--!EXEC ShtoPunetor 
 --   @id = '456',
 --   @emri = 'Majlinda Axhiu',
  --  @contact_info = 'Majlina.axhiu@unt.edu.mk',
  --  @data_punsimit = '2025-06-02';
--	
	GO

CREATE PROCEDURE ShfaqShitjetDitore
    @data DATE
AS
BEGIN
    SELECT id, datakoha_shitjes, cmimi_total, puntoret_id
    FROM shitjet
    WHERE CAST(datakoha_shitjes AS DATE) = @data;
END;

EXEC ShfaqShitjetDitore @data = '2024-05-21';

	GO

	CREATE TRIGGER trg_CmimiTotalShitjeArtikull
ON shitje_artikuj
AFTER INSERT
AS
BEGIN
    UPDATE sa
    SET sa.cmimi_total = sa.sasia * sa.cmimi_njesi
    FROM shitje_artikuj sa
    INNER JOIN inserted i ON sa.id = i.id;
END;