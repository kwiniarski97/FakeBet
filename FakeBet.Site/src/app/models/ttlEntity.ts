export class TtlEntity {
  timeStamp: number;
  content: any;


  constructor(timeStamp: number, content: any) {
    this.timeStamp = timeStamp;
    this.content = content;
  }
}
