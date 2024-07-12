import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

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
  public forecasts: WeatherForecast[] = [];
  public credentials: CredentialDto[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCredentials();
    this.getForecasts();
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


  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'angularapp1.client';
}
