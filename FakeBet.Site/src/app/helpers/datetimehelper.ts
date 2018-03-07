export class DateTimeHelper {
  public ParseDate(dateStr: Date): string {
    const date = new Date(dateStr);
    let sb = '';
    const separator = ':';
    sb += date.getHours();
    sb += separator;
    sb += (date.getMinutes() < 10 ? `0${date.getMinutes()}` : date.getMinutes());
    sb += ` ${date.toLocaleDateString()}`;
    return sb;
  }
}
