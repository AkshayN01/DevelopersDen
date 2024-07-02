import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenericService {

  constructor() { }
  convertDate(dateString: string): string {
    if(dateString == null || dateString == undefined || dateString == '')
      return '';

    // Create a new Date object
    const date = new Date(dateString);

    // Get the day, month, and year components
    const day = ("0" + date.getDate()).slice(-2); // Zero padding
    const month = ("0" + (date.getMonth() + 1)).slice(-2); // Months are zero-based
    const year = date.getFullYear();

    // Get the hours, minutes, and seconds components
    const hours = ("0" + date.getHours()).slice(-2);
    const minutes = ("0" + date.getMinutes()).slice(-2);
    const seconds = ("0" + date.getSeconds()).slice(-2);

    // Format the date in the desired format
    const formattedDate = `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;

    console.log(formattedDate); // Output: "01/04/2024 00:00:00"

    return formattedDate;
  }
  
  getRequestOptions() {
    // You can add any headers or authentication tokens here
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      // Add any additional headers here
    });
    return { headers };
  }
  handleError(error: any) {
    if (error.error instanceof ErrorEvent) 
    {
      console.error('Client-side error:', error.error.message);
    } 
    else 
    {
      console.error('Server-side error:', error.status, error.error);
    }
    return throwError(() => error);
  }
}
