import axios from "axios";

export async function fetchProducts(discountType: string, filteredBy: string, orderBy: string, page: number, pageSize: string, onSetValues: (totalCount: number, pageCount: number) => void): Promise<any> {
  const queryParams = new URLSearchParams({
    pageNumber: page.toString(),
    pageSize: pageSize,
    discount: discountType,
    filteredBy: filteredBy,
    orderBy: orderBy
  });

  const apiUrl = `http://localhost:5001/api/search?${queryParams.toString()}`;
  const response = await axios.get(apiUrl);

  onSetValues(response.data.totalCount, response.data.pageCount);
  return response.data;
}