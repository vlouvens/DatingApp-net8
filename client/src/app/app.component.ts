import { HttpClient,provideHttpClient } from '@angular/common/http';
import { Component,inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  //imports: [RouterOutlet],
  providers: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'DattingApp';
  users:any;
  http = inject(HttpClient);
  
  ngOnInit(): void {
    this.http.get("https://localhost:5001/api/users/").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("Request complete")
    })
  }
}
