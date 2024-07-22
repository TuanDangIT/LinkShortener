import { HttpClient } from '@angular/common/http';
import { Component, DestroyRef, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { catchError, throwError } from 'rxjs';
@Component({
  selector: 'app-link-shortner',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './link-shortner.component.html',
  styleUrl: './link-shortner.component.css',
})
export class LinkShortnerComponent {
  url = signal('');
  shortenedUrl = signal('');
  private httpClient = inject(HttpClient);
  private destroyRef = inject(DestroyRef);
  onLinkSubmit() {
    const baseUrl = 'https://localhost:44366';
    const subscription = this.httpClient
      .post<string>(`${baseUrl}/shorten?url=${this.url()}`, {})
      // .pipe(
      //   catchError((error) => {
      //     return throwError(() => {
      //       new Error('Something went wrong');
      //     });
      //   })
      // )
      .subscribe({
        next: (resData) => {
          this.shortenedUrl.set(resData);
        },
        error: () => {
          console.log('Something went wrong');
        },
      });
    this.url.set('');
  }
}
