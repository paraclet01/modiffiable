--ALTER TABLE Agences ALTER COLUMN Statut int;
--update Agences set Statut = 0

ALTER TABLE Parametres ALTER COLUMN CodeModifiable int;
update Parametres set CodeModifiable = 0 where id in (1,2)
update Parametres set CodeModifiable = 1 where id in (3,4)