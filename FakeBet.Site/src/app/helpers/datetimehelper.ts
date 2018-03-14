export class DateTimeHelper {
  public ParseDate(dateStr: Date): string {
    const date = new Date(dateStr);
    let sb = '';
    const timeSeparator = ':';
    const dateSeparator = '.';
    sb += date.getHours();
    sb += timeSeparator;
    sb += this.formatDateIfUnder10(date.getMinutes());
    sb += ' ';
    sb += this.formatDateIfUnder10(date.getDay());
    sb += dateSeparator;
    sb += this.formatDateIfUnder10(date.getMonth());
    sb += dateSeparator;
    sb += date.getFullYear();
    return sb;
  }

  private formatDateIfUnder10(date: number): string {
    return (date < 10 ? `0${date}` : `${date}`);
  }
}
