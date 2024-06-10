import os
from github import Github
import base64

# Autenticación
token = os.getenv("GITHUB_TOKEN")
g = Github(token)

# Obtener el repositorio
repo = g.get_user().get_repo("VINF04394-TP3-Sistemas-Colaborativos")

def upload_file(file_path, commit_message):
    with open(file_path, "rb") as file:
        content = file.read()
    encoded_content = base64.b64encode(content).decode()
    repo.create_file(file_path, commit_message, encoded_content)
    print(f"Archivo {file_path} subido con éxito.")

def update_file(file_path, commit_message):
    contents = repo.get_contents(file_path)
    with open(file_path, "rb") as file:
        content = file.read()
    encoded_content = base64.b64encode(content).decode()
    repo.update_file(contents.path, commit_message, encoded_content, contents.sha)
    print(f"Archivo {file_path} actualizado con éxito.")

def delete_file(file_path, commit_message):
    contents = repo.get_contents(file_path)
    repo.delete_file(contents.path, commit_message, contents.sha)
    print(f"Archivo {file_path} eliminado con éxito.")

# Ejemplos de uso:
# upload_file("ruta/del/archivo.txt", "Agregar archivo")
# update_file("ruta/del/archivo.txt", "Actualizar archivo")
# delete_file("ruta/del/archivo.txt", "Eliminar archivo")

if __name__ == "__main__":
    # Descomenta la línea según la operación que quieras realizar
    # upload_file("ruta/del/archivo.txt", "Agregar archivo")
    # update_file("ruta/del/archivo.txt", "Actualizar archivo")
    # delete_file("ruta/del/archivo.txt", "Eliminar archivo")
    pass
