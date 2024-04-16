USE TaskManager
GO

INSERT INTO TEMPLATE(TemplateName)
VALUES('Default'),
      ('IT Management'),
      ('Design Management')

INSERT INTO LISTTEMPLATE(IDTemplate, StatusName)
VALUES(1, 'To Do'),
      (1, 'In Progress'),
      (1, 'In Preview'),
      (1, 'Done'),

      (2, 'To Do'),
      (2, 'In Progress'),
      (2, 'In Preview'),
      (2, 'Test'),
      (2, 'Bug'),
      (2, 'Done'),

      (3, 'To Do'),
      (3, 'Idea'),
      (3, 'Sketch'),
      (3, 'In Preview'),
      (3, 'Done')