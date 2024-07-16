import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface CredentialDto {
  id: number;
  name: string;
  password: string;
  creationTime: Date;
  isPasswordVisible: Boolean;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public credentials: CredentialDto[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCredentials();
  }

  getCredentials(filter?: string) {
    let url = '/credential';

    if (filter) {
      url += `?name=${filter}`;
    }

    this.http.get<CredentialDto[]>(url).subscribe(
      (result) => {
        this.credentials = result;
      },
      (error) => {
        console.error('Ошибка при получении учетных данных:', error);
      }
    );
  }

  hidePassword(credential: CredentialDto): string {
    return credential.isPasswordVisible ? credential.password : credential.password.replace(/./g, '*');
  }

  togglePasswordVisibility(credential: CredentialDto) {
    credential.isPasswordVisible = !credential.isPasswordVisible;
  }


  title = 'angularapp1.client';
}
