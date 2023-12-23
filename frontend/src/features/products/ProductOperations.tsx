import { Filter } from "../../ui/Filter";
import PageSize from "../../ui/PageSize";
import SortBy from "../../ui/SortBy";
import TableOperations from "../../ui/TableOperations";

export const ProductTableOperations = () => {
  return (
    <TableOperations>
      <Filter
        filterField="filteredBy"
        options={[
          { value: "all", label: "All" },
          { value: "active", label: "Active" },
          { value: "inactive", label: "Inactive" },
          { value: "available", label: "With Stock" },
        ]}
      />

      <SortBy
        options={[
          { value: "name-asc", label: "Sort by name (A-Z)" },
          { value: "name-desc", label: "Sort by name (Z-A)" },
          { value: "price-asc", label: "Sort by price (low first)" },
          { value: "price-desc", label: "Sort by price (high first)" },
          { value: "category-asc", label: "Sort by category (A-Z)" },
          { value: "category-desc", label: "Sort by category (Z-A)" },
        ]}
      />

      <PageSize
        options={[
          { value: "5", label: "Page Size: 5" },
          { value: "10", label: "Page Size: 10" },
          { value: "20", label: "Page Size: 20" },
          { value: "50", label: "Page Size: 50" },
          { value: "100", label: "Page Size: 100" },
        ]}
      />
    </TableOperations>
  );
};
