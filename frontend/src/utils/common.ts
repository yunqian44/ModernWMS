import { SearchObject, SearchOperator } from '@/types/System/Form'

export const setSearchObject = (searchForm: any) => {
  const searchObjects: Array<SearchObject> = []
  try {
    for (const key in searchForm) {
      const str = key as string
      const searchValue = searchForm[str as keyof typeof searchForm]

      if (searchValue && searchValue.trim() !== '') {
        searchObjects.push({
          name: key,
          operators: SearchOperator.INCLUDE,
          text: searchValue,
          value: searchValue
        })
      }
    }
    return searchObjects
  } catch (error) {
    return searchObjects
  }
}
