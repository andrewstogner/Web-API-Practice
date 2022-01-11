import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl = "http://localhost:50327/api";
readonly PhotoUrl = "http://localhost:50327/Photos";

  constructor(private http:HttpClient) { }

  //Department
  getDepList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDep(val:any){
    return this.http.post(this.APIUrl + '/department', val);
  }
  
  updateDep(val:any){
    return this.http.put(this.APIUrl + '/department', val);
  }

  delDep(val:any){
    return this.http.delete(this.APIUrl + '/department/' + val);
  }

  //Employee
  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/employee');
  }

  addEmp(val:any){
    return this.http.post(this.APIUrl + '/employee', val);
  }
  
  updateEmp(val:any){
    return this.http.put(this.APIUrl + '/employee', val);
  }

  delEmp(val:any){
    return this.http.delete(this.APIUrl + '/employee/' + val);
  }

  uploadPhoto(val:any){
    return this.http.post(this.APIUrl + '/employee/SaveFile',val);
  }

  getAllDepNames():Observable<any[]>{
    return this.http.get<any[]>(this.APIUrl + '/employee/GetAllDepartmentNames');
  }
}
