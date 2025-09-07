import http.server
import socketserver
import os
import urllib.parse

PORT = 8000
FILENAME = "manifest.json"
BUILD_DIR = "build_scripts"

class MyHandler(http.server.SimpleHTTPRequestHandler):
    def do_GET(self):
        # Normalize and decode path
        path = urllib.parse.unquote(self.path)   # decode %20 -> space
        path = os.path.normpath(path.lstrip("/"))  # remove leading slashes, normalize

        if path == FILENAME:
            # Serve testing.json
            self.send_response(200)
            self.send_header("Content-type", "application/json")
            self.end_headers()
            with open(FILENAME, "rb") as f:
                self.wfile.write(f.read())
        elif path.startswith(BUILD_DIR + os.sep):
            # Serve files inside build_scripts
            if os.path.isfile(path):
                self.send_response(200)
                self.send_header("Content-type", self.guess_type(path))
                self.end_headers()
                with open(path, "rb") as f:
                    self.wfile.write(f.read())
            else:
                self.send_error(404, "File not found.")
        else:
            self.send_error(404, "File not found.")

class ReusableTCPServer(socketserver.TCPServer):
    allow_reuse_address = True

with ReusableTCPServer(("", PORT), MyHandler) as httpd:
    print(f"Serving {FILENAME} at http://localhost:{PORT}/{FILENAME}")
    print(f"Serving files from '{BUILD_DIR}' at http://localhost:{PORT}/{BUILD_DIR}/<filename>")
    httpd.serve_forever()
