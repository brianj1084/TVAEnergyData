export interface Series {
  seriesId: string;
  name: string;
  units: string;
  description: string;
  start: string;
  end: string;
  updated: string;
  series: Series[];
}
